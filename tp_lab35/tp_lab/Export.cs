using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using common;
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace tp_lab {
	class Export {
		public static bool OrdersCSV(string fname, int skip=0, int take=int.MaxValue, Action<float> cb = null)
		{
			StreamWriter w = new StreamWriter(fname, false, Encoding.UTF8);

			using (Entities ctx=new Entities()) {
				try {
					IQueryable<Order> query = ctx.order.Include("products").Include("status").Include("customer").Include("products.product").AsNoTracking().OrderByDescending(x => x.date).Skip(skip);

					if (take>=0) query = query.Take(take);

					int progress=0;
					int count = query.Count()+1;

					foreach (Order o in query) {
						string info = string.Format(
							"№ заказа;{0}\nДата;{1}\nСтатус;{2}\nКод покупателя;{3}\nФИО;{4};{5};{6}\nТелефон;{7}\nEmail;{8}\nСтрана;Город;Адрес;Почтовый индекс\n{9};{10};{11};{12}\n",
							o.id,
							o.date,
							o.status.name,
							o.customer_id,
							o.last_name,
							o.first_name,
							o.middle_name,
							o.customer.phone,
							o.customer.email,
							o.country,
							o.city,
							o.address,
							o.postcode
							);

						w.Write(info);
						w.Write("Код товара;Наименование;Количество;Цена\n");

						decimal total=0;

						foreach (OrderProduct op in o.products) {
							string pinfo = string.Format(
								"{0};{1};{2};{3}\n",
								op.product.id,
								op.product.name,
								op.quantity,
								op.price
								);

							w.Write(pinfo);

							total += op.price * op.quantity;
						}

						w.Write(string.Format(";;Итого;{0}\n\n", total));

						++progress;
						if (cb!=null) cb(progress / (float)count);
					}

					w.Flush();
					++progress;
					if (cb!=null) cb(progress / (float)count);

					w.Close();

					return true;
				} catch (Exception ex) {
					ex.Log();
				}

				w.Close();
				return false;
			}
		}

		public static bool OrdersExcelInterop(string filename, int skip=0, int take=int.MaxValue, Action<float> cb = null)
		{
			//if (s==null) throw new ArgumentNullException();
			//if (!s.CanWrite) throw new ArgumentException("В поток нельзя писать");


			using (Entities ctx=new Entities()) {
				ctx.Configuration.AutoDetectChangesEnabled = false;

				try {
					IQueryable<Order> query = ctx.order.Include("products").Include("status").Include("customer").Include("products.product").AsNoTracking().OrderByDescending(x => x.date).Skip(skip);


					if (take>=0) query = query.Take(take);

					// Стартуем приложение
					var xApp = new Microsoft.Office.Interop.Excel.Application();
					//xApp.Visible = true;

					var xWB = xApp.Workbooks.Add("");
					var ws = (Microsoft.Office.Interop.Excel._Worksheet)xWB.ActiveSheet;

					int offset = 1;

					Microsoft.Office.Interop.Excel.Range rng;

					List<Order> list = query.ToList();

					int progress = 0;
					int count = list.Count+1;

					foreach (Order o in list) {
						// Заказ
						ws.Cells[offset, 1] = "№ заказа";
						ws.Cells[offset, 2] = "Дата";
						ws.Cells[offset, 3] = "Статус";

						ws.Cells[offset+1, 1] = o.id;
						ws.Cells[offset+1, 2] = o.date;
						ws.Cells[offset+1, 3] = o.status.name;

						rng = ws.Range[ws.Cells[offset, 1], ws.Cells[offset, 3]];
						rng.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
						offset += 3;

						// Покупатель
						ws.Cells[offset, 1] = "Покупатель";
						ws.Cells[offset, 2] = "Фамилия";
						ws.Cells[offset, 3] = "Имя";
						ws.Cells[offset, 4] = "Отчество";
						ws.Cells[offset, 5] = "Телефон";
						ws.Cells[offset, 6] = "Email";

						ws.Cells[offset+1, 1] = o.customer_id;
						ws.Cells[offset+1, 2] = o.last_name;
						ws.Cells[offset+1, 3] = o.first_name;
						ws.Cells[offset+1, 4] = o.middle_name;
						ws.Cells[offset+1, 5] = o.customer.phone;
						ws.Cells[offset+1, 6] = o.customer.email;

						rng = ws.Range[ws.Cells[offset, 1], ws.Cells[offset, 6]];
						rng.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
						offset += 3;

						// Доставка
						ws.Cells[offset, 1] = "Страна";
						ws.Cells[offset, 2] = "Город";
						ws.Cells[offset, 3] = "Адрес";
						ws.Cells[offset, 4] = "Почтовый индекс";

						ws.Cells[offset+1, 1] = o.country;
						ws.Cells[offset+1, 2] = o.city;
						ws.Cells[offset+1, 3] = o.address;
						ws.Cells[offset+1, 4] = o.postcode;

						rng = ws.Range[ws.Cells[offset, 1], ws.Cells[offset, 4]];
						rng.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
						offset += 3;

						// Заголовки
						ws.Cells[offset, 1] = "Код товара";
						ws.Cells[offset, 2] = "Название";
						ws.Cells[offset, 3] = "Количество";
						ws.Cells[offset, 4] = "Цена";


						int i=1;
						decimal total = 0;

						foreach (OrderProduct op in o.products) {
							ws.Cells[offset+i, 1] = op.product_id;
							ws.Cells[offset+i, 2] = op.product.name;
							ws.Cells[offset+i, 3] = op.quantity;
							ws.Cells[offset+i, 4] = op.price;

							total += op.price*op.quantity;
							++i;
						}

						offset += i;

						rng = ws.Range[ws.Cells[offset-1, 1], ws.Cells[offset-1, 4]];
						rng.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
						rng.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick;

						ws.Cells[offset, 3] = "Итого";
						ws.Cells[offset, 4] = total;

						offset += 3;

						++progress;
						if (cb!=null) cb(progress / (float)count);
					}

					xApp.Visible = false;
					xApp.UserControl = false;

					xWB.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
						false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared,
						Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

					xWB.Close();

					++progress;
					if (cb!=null) cb(progress/ (float)count);

					return true;
				} catch (Exception ex) {
					ex.Log();
				}

				return false;
			}


		}

		public static bool OrdersExcelDOM(string filename, int skip=0, int take=int.MaxValue, Action<float> cb = null)
		{
			//if (s==null) throw new ArgumentNullException();
			//if (!s.CanWrite) throw new ArgumentException("В поток нельзя писать");


			using (Entities ctx=new Entities()) {
				ctx.Configuration.AutoDetectChangesEnabled = false;
				ctx.Database.CommandTimeout = 180;

				try {
					IQueryable<Order> query = ctx.order.Include("products").Include("status").Include("customer").Include("products.product").AsNoTracking().OrderByDescending(x => x.date).Skip(skip);

					if (take>=0) query = query.Take(take);

					var xWB = new XLWorkbook();//xApp.Workbooks.Add("");
					var ws = xWB.AddWorksheet("Заказы");//(Microsoft.Office.Interop.Excel._Worksheet)xWB.ActiveSheet;					

					int offset = 1;

					IXLRange rng;

					var list = query;//.ToList();

					int progress = 0;
					int count = list.Count()+1;



					foreach (Order o in list) {
						// Заказ
						ws.Cell(offset, 1).Value = "№ заказа";
						ws.Cell(offset, 2).Value = "Дата";
						ws.Cell(offset, 3).Value = "Статус";

						ws.Cell(offset+1, 1).Value = o.id;
						ws.Cell(offset+1, 2).Value = o.date;
						ws.Cell(offset+1, 3).Value = o.status.name;

						rng = ws.Range(offset, 1, offset+1, 4);
						rng.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
						offset += 2;

						// Покупатель
						ws.Cell(offset, 1).Value = "Покупатель";
						ws.Cell(offset, 2).Value = "Фамилия";
						ws.Cell(offset, 3).Value = "Имя";
						ws.Cell(offset, 4).Value = "Отчество";
						//ws.Cell(offset, 5).Value = "Телефон";
						//ws.Cell(offset, 6).Value = "Email";

						ws.Cell(offset+1, 1).Value = o.customer_id;
						ws.Cell(offset+1, 2).Value = o.last_name;
						ws.Cell(offset+1, 3).Value = o.first_name;
						ws.Cell(offset+1, 4).Value = o.middle_name;
						//ws.Cell(offset+1, 5).Value = o.customer.phone;
						//ws.Cell(offset+1, 6).Value = o.customer.email;

						rng = ws.Range(offset, 1, offset+1, 4);
						rng.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
						offset += 2;

						// Доставка
						ws.Cell(offset, 1).Value = "Страна";
						ws.Cell(offset, 2).Value = "Город";
						ws.Cell(offset, 3).Value = "Адрес";
						ws.Cell(offset, 4).Value = "Почтовый индекс";

						ws.Cell(offset+1, 1).Value = o.country;
						ws.Cell(offset+1, 2).Value = o.city;
						ws.Cell(offset+1, 3).Value = o.address;
						ws.Cell(offset+1, 4).Value = o.postcode;
						offset -= 4;

						rng = ws.Range(offset, 1, offset+6, 4);
						rng.Style.Border.TopBorder = XLBorderStyleValues.Thin;
						rng.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
						rng.Style.Border.RightBorder = XLBorderStyleValues.Thin;

						// Заголовки
						ws.Cell(offset, 5).Value = "Код товара";
						ws.Cell(offset, 6).Value = "Название";
						ws.Cell(offset, 7).Value = "Количество";
						ws.Cell(offset, 8).Value = "Цена";

						rng = ws.Range(offset, 5, offset+1, 8);
						rng.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

						int i=1;
						decimal total = 0;

						foreach (OrderProduct op in o.products) {
							ws.Cell(offset+i, 5).Value = op.product_id;
							ws.Cell(offset+i, 6).Value = op.product.name;
							ws.Cell(offset+i, 7).Value = op.quantity;
							ws.Cell(offset+i, 8).Value = op.price;

							total += op.price*op.quantity;
							++i;
						}

						ws.Cell(offset+i, 7).Value = "Итого";
						ws.Cell(offset+i, 8).Value = total;

						// Толстая линия
						rng = ws.Range(offset+i-1, 5, offset+i-1, 8);
						rng.Style.Border.BottomBorder = XLBorderStyleValues.Thick;

						// Линия вокруг товаров
						rng = ws.Range(offset, 5, offset+i, 8);
						rng.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

						offset += Math.Max(i, 6) + 2;

						++progress;
						if (cb!=null) cb(progress / (float)count);
					}

					xWB.SaveAs(filename);

					++progress;
					if (cb!=null) cb(progress/ (float)count);

					return true;
				} catch (Exception ex) {
					ex.Log();
				}

				return false;
			}
		}

		public static bool OrdersExcel(string filename, int skip, int take, System.Threading.CancellationToken ct, bool bRelated = true, Action<float> cb = null)
		{
			bool result = false;

			try {
				using (SpreadsheetDocument doc = SpreadsheetDocument.Create(filename, SpreadsheetDocumentType.Workbook)) {
					OpenXmlWriter w;

					doc.AddWorkbookPart();
					WorksheetPart workSheetPart = doc.WorkbookPart.AddNewPart<WorksheetPart>();

					w = OpenXmlWriter.Create(workSheetPart);
					w.WriteStartElement(new DocumentFormat.OpenXml.Spreadsheet.Worksheet());
					w.WriteStartElement(new DocumentFormat.OpenXml.Spreadsheet.SheetData());
					
					System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
					double  dbtime = 0,
							dbtime2 = 0,
							mytime = 0,
							mytime2 = 0,
							writetime = 0;

					sw.Start();

					int progress = 0;
					int count = 1;

					using (Entities ctx=new Entities()) {
						ctx.Configuration.AutoDetectChangesEnabled = false;
						ctx.Database.CommandTimeout = 180;

						ctx.Database.Log = x => LogFile.Log(x);

						Dictionary<int, string> statuses = ctx.order_status.ToDictionary(x => x.id, x => x.name);
						
						IQueryable<Order> query = (bRelated ? ctx.order.Include("products").Include("products.product")/*.Include("customer")/**/ : ctx.order).AsNoTracking().OrderByDescending(x => x.date).Skip(skip);
						if (take>=0) query = query.Take(take);
						
						var list = query.ToList();

						int offset = 1;
						count = list.Count()+1;

						DumpTimeAndRestart(sw, ref dbtime);

						List<Row> rows = new List<Row>(6);

						// Заказ и товары
						if (bRelated) {
							rows.Add(new Row(
								new Cell { DataType = CellValues.String, StyleIndex = 1, CellValue = new CellValue("№ заказа") },
								new Cell { DataType = CellValues.String, StyleIndex = 2, CellValue = new CellValue("Дата") },
								new Cell { DataType = CellValues.String, StyleIndex = 2, CellValue = new CellValue("Статус") },
								new Cell { StyleIndex = 3 },

								new Cell { DataType = CellValues.String, StyleIndex = 2, CellValue = new CellValue("Код товара") },
								new Cell { DataType = CellValues.String, StyleIndex = 2, CellValue = new CellValue("Название") },
								new Cell { DataType = CellValues.String, StyleIndex = 2, CellValue = new CellValue("Количество") },
								new Cell { DataType = CellValues.String, StyleIndex = 3, CellValue = new CellValue("Цена") }
							));
						} else {
							rows.Add(new Row(
								new Cell { DataType = CellValues.String, StyleIndex = 1, CellValue = new CellValue("№ заказа") },
								new Cell { DataType = CellValues.String, StyleIndex = 2, CellValue = new CellValue("Дата") },
								new Cell { DataType = CellValues.String, StyleIndex = 2, CellValue = new CellValue("Статус") },
								new Cell { StyleIndex = 3 }
							));
						}

						// Данные заказа
						rows.Add(new Row(
							new Cell { DataType = CellValues.String, StyleIndex = 8, CellValue = new CellValue() },
							new Cell { DataType = CellValues.String, StyleIndex = 0, CellValue = new CellValue() },
							new Cell { DataType = CellValues.String, StyleIndex = 0, CellValue = new CellValue() },
							new Cell { StyleIndex = 4 }
						));

						// Данные покупателя
						rows.Add(new Row(
							new Cell { DataType = CellValues.String, StyleIndex = 1, CellValue = new CellValue("Покупатель") },
							new Cell { DataType = CellValues.String, StyleIndex = 2, CellValue = new CellValue("Фамилия") },
							new Cell { DataType = CellValues.String, StyleIndex = 2, CellValue = new CellValue("Имя") },
							new Cell { DataType = CellValues.String, StyleIndex = 3, CellValue = new CellValue("Отчество") }
						));

						rows.Add(new Row(
							new Cell { DataType = CellValues.String, StyleIndex = 8, CellValue = new CellValue() },
							new Cell { DataType = CellValues.String, StyleIndex = 0, CellValue = new CellValue() },
							new Cell { DataType = CellValues.String, StyleIndex = 0, CellValue = new CellValue() },
							new Cell { DataType = CellValues.String, StyleIndex = 4, CellValue = new CellValue() }
						));

						// Данные адреса
						rows.Add(new Row(
							new Cell { DataType = CellValues.String, StyleIndex = 1, CellValue = new CellValue("Страна") },
							new Cell { DataType = CellValues.String, StyleIndex = 2, CellValue = new CellValue("Город") },
							new Cell { DataType = CellValues.String, StyleIndex = 2, CellValue = new CellValue("Адрес") },
							new Cell { DataType = CellValues.String, StyleIndex = 3, CellValue = new CellValue("Почтовый индекс") }
						));

						rows.Add(new Row(
							new Cell { DataType = CellValues.String, StyleIndex = 7, CellValue = new CellValue() },
							new Cell { DataType = CellValues.String, StyleIndex = 6, CellValue = new CellValue() },
							new Cell { DataType = CellValues.String, StyleIndex = 6, CellValue = new CellValue() },
							new Cell { DataType = CellValues.String, StyleIndex = 5, CellValue = new CellValue() }
						));

						DumpTimeAndRestart(sw, ref mytime);

						foreach (Order o in list) {
							DumpTimeAndRestart(sw, ref dbtime);

							if (ct.IsCancellationRequested) break;

							{
								Row r;
								Cell c;

								r = rows[0];
								r.RowIndex = (uint)offset;	
							
								// Заказ
								r = rows[1];
								c = r.GetFirstChild<Cell>();	c.CellValue.Text = o.id.ToString();							
								c = c.NextSibling<Cell>();		c.CellValue.Text = o.date.ToShortDateString();								
								c = c.NextSibling<Cell>();		c.CellValue.Text = statuses[o.status_id];

								// Покупатель
								r = rows[3];								
								c = r.GetFirstChild<Cell>();	c.CellValue.Text = o.customer_id.ToString();								
								c = c.NextSibling<Cell>();		c.CellValue.Text = o.last_name;								
								c = c.NextSibling<Cell>();		c.CellValue.Text = o.first_name;
								c = c.NextSibling<Cell>();		c.CellValue.Text = o.middle_name;

								//Доставка
								r = rows[5];								
								c = r.GetFirstChild<Cell>();	c.CellValue.Text = o.country;
								c = c.NextSibling<Cell>();		c.CellValue.Text = o.city;
								c = c.NextSibling<Cell>();		c.CellValue.Text = o.address;
								c = c.NextSibling<Cell>();		c.CellValue.Text = o.postcode;

							}

							int i=1;
							decimal total = 0;

							// Очищаем шаблон
							if (rows.Count>6)
								rows.RemoveRange(6, rows.Count-6);

							for (int j=1; j<6; ++j) {
								for (int k=rows[j].Elements<Cell>().Count()-1; k>=4; --k)
									rows[j].RemoveChild<Cell>((Cell)rows[j].Elements<Cell>().ElementAt(k));
							}

							DumpTimeAndRestart(sw, ref mytime);

							if (bRelated) {
								foreach (OrderProduct op in o.products) {
									//ctx.Entry<OrderProduct>(op).Reference(x => x.product).Load();

									DumpTimeAndRestart(sw, ref dbtime2);

									Row r;

									// Выбираем или создаём новую строку
									if (i<rows.Count)
										r = rows[i];
									else {
										r = new Row();
										rows.Add(r);
									}

									r.Append(
										new Cell { DataType = CellValues.String, StyleIndex = 8, CellValue = new CellValue(op.product_id.ToString()), CellReference = "E" + (offset+i).ToString() },
										new Cell { DataType = CellValues.String, StyleIndex = 0, CellValue = new CellValue(op.product.name) },
										new Cell { DataType = CellValues.String, StyleIndex = 0, CellValue = new CellValue(op.quantity.ToString()) },
										new Cell { DataType = CellValues.String, StyleIndex = 4, CellValue = new CellValue(op.price.ToString()) }
									);

									total += op.price*op.quantity;
									++i;

									DumpTimeAndRestart(sw, ref mytime2);
								}

								{ // Итого
									Row r;

									// Выбираем или создаём новую строку
									if (i<rows.Count)
										r = rows[i];
									else {
										r = new Row();
										rows.Add(r);
									}

									r.Append(
										new Cell { StyleIndex = 9, CellReference = "E" + (offset+i).ToString() },
										new Cell { StyleIndex = 10 },
										new Cell { DataType = CellValues.String, StyleIndex = 10, CellValue = new CellValue("Итого") },
										new Cell { DataType = CellValues.String, StyleIndex = 11, CellValue = new CellValue(total.ToString()) }
									);
								}
							}

							offset += Math.Max(i, 6) + 1;

							DumpTimeAndRestart(sw, ref mytime);

							// Записываем порцию
							foreach (Row r in rows) {
								w.WriteElement(r);
							}

							DumpTimeAndRestart(sw, ref writetime);

							++progress;
							if (cb!=null) cb(progress / (float)count);
						}


					}

					// Закрываем SheetData
					w.WriteEndElement();
					// Закрываем Worksheet
					w.WriteEndElement();
					w.Close();

					// Создаём стили
					var styles = doc.WorkbookPart.AddNewPart<WorkbookStylesPart>();
					styles.Stylesheet = new Stylesheet(
						new Fonts(
							new Font(
								new FontSize { Val = 11 },
								new Color { Auto = true },
								new FontName { Val = "Calibri" }
							)
						) { Count = 1 },
						new Fills(
							new Fill(
								new PatternFill() { PatternType = PatternValues.None }
							),
							new Fill(
								new PatternFill() { PatternType = PatternValues.Gray125 }
							)
						) { Count = 2 },
						new Borders(
							new Border(
								new LeftBorder(),
								new RightBorder(),
								new TopBorder(),
								new BottomBorder(),
								new DiagonalBorder()
							),
							new Border(
								new LeftBorder { Style = BorderStyleValues.Thin },
								new RightBorder(),
								new TopBorder { Style = BorderStyleValues.Thin },
								new BottomBorder(),
								new DiagonalBorder()
							),
							new Border(
								new LeftBorder(),
								new RightBorder(),
								new TopBorder { Style = BorderStyleValues.Thin },
								new BottomBorder(),
								new DiagonalBorder()
							),
							new Border(
								new LeftBorder(),
								new RightBorder { Style = BorderStyleValues.Thin },
								new TopBorder { Style = BorderStyleValues.Thin },
								new BottomBorder(),
								new DiagonalBorder()
							),
							new Border(
								new LeftBorder(),
								new RightBorder { Style = BorderStyleValues.Thin },
								new TopBorder(),
								new BottomBorder(),
								new DiagonalBorder()
							),
							new Border(
								new LeftBorder(),
								new RightBorder { Style = BorderStyleValues.Thin },
								new TopBorder(),
								new BottomBorder { Style = BorderStyleValues.Thin },
								new DiagonalBorder()
							),
							new Border(
								new LeftBorder(),
								new RightBorder(),
								new TopBorder(),
								new BottomBorder { Style = BorderStyleValues.Thin },
								new DiagonalBorder()
							),
							new Border(
								new LeftBorder { Style = BorderStyleValues.Thin },
								new RightBorder(),
								new TopBorder(),
								new BottomBorder { Style = BorderStyleValues.Thin },
								new DiagonalBorder()
							),
							new Border(
								new LeftBorder { Style = BorderStyleValues.Thin },
								new RightBorder(),
								new TopBorder(),
								new BottomBorder(),
								new DiagonalBorder()
							),

							new Border(
								new LeftBorder { Style = BorderStyleValues.Thin },
								new RightBorder(),
								new TopBorder { Style = BorderStyleValues.Thin },
								new BottomBorder { Style = BorderStyleValues.Thin },
								new DiagonalBorder()
							),
							new Border(
								new LeftBorder(),
								new RightBorder(),
								new TopBorder { Style = BorderStyleValues.Thin },
								new BottomBorder { Style = BorderStyleValues.Thin },
								new DiagonalBorder()
							),
							new Border(
								new LeftBorder(),
								new RightBorder { Style = BorderStyleValues.Thin },
								new TopBorder { Style = BorderStyleValues.Thin },
								new BottomBorder { Style = BorderStyleValues.Thin },
								new DiagonalBorder()
							)
						) { Count = 12 },
						new CellFormats(
							new CellFormat { FontId = 0, FillId = 0, BorderId = 0 },
							new CellFormat { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true },
							new CellFormat { FontId = 0, FillId = 0, BorderId = 2, ApplyBorder = true },
							new CellFormat { FontId = 0, FillId = 0, BorderId = 3, ApplyBorder = true },
							new CellFormat { FontId = 0, FillId = 0, BorderId = 4, ApplyBorder = true },
							new CellFormat { FontId = 0, FillId = 0, BorderId = 5, ApplyBorder = true },
							new CellFormat { FontId = 0, FillId = 0, BorderId = 6, ApplyBorder = true },
							new CellFormat { FontId = 0, FillId = 0, BorderId = 7, ApplyBorder = true },
							new CellFormat { FontId = 0, FillId = 0, BorderId = 8, ApplyBorder = true },
							new CellFormat { FontId = 0, FillId = 0, BorderId = 9, ApplyBorder = true },
							new CellFormat { FontId = 0, FillId = 0, BorderId = 10, ApplyBorder = true },
							new CellFormat { FontId = 0, FillId = 0, BorderId = 11, ApplyBorder = true }
						) { Count = 12 }
					);

					//styles.Stylesheet.Save();

					w = OpenXmlWriter.Create(styles);
					w.WriteElement(styles.Stylesheet);
					w.Close();

					w = OpenXmlWriter.Create(doc.WorkbookPart);
					w.WriteStartElement(new DocumentFormat.OpenXml.Spreadsheet.Workbook());
					w.WriteStartElement(new DocumentFormat.OpenXml.Spreadsheet.Sheets());

					w.WriteElement(new DocumentFormat.OpenXml.Spreadsheet.Sheet() {
						Name = "Заказы",
						SheetId = 1,
						Id = doc.WorkbookPart.GetIdOfPart(workSheetPart)
					});

					w.WriteEndElement();
					w.WriteEndElement();

					w.Close();
					doc.Close();
					
					++progress;
					if (cb!=null) cb(progress/ (float)count);

					DumpTimeAndRestart(sw, ref writetime);
					
					System.Diagnostics.Debug.WriteLine(string.Format("db: {0} db2: {1} my: {2} my2: {3} write: {4}", dbtime, dbtime2, mytime, mytime2, writetime));
					System.Diagnostics.Debug.WriteLine(string.Format("Total: {0}", dbtime+dbtime2+mytime+mytime2+writetime));
				}

				result = true;
			} catch (Exception ex) {
				ex.Log();
			}

			return result;
		}

		static void DumpTimeAndRestart(System.Diagnostics.Stopwatch s, ref double v)
		{
			v += s.Elapsed.TotalSeconds;
			s.Restart();
		}
	}
}
