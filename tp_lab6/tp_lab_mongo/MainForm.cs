using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.ServiceModel;
using Npgsql;
using NpgsqlTypes;
using common;
using System.Web;
using tp_lab.misha_api;
using tp_lab.mongo;
using MongoDB.Driver;

namespace tp_lab {
	public partial class MainForm : Form {
		Dictionary<string, IController> ctrls;
		bool bAPISubj;

		MongoContext mCtx;

		public MainForm()
		{
			InitializeComponent();
			ctrls = new Dictionary<string, IController>();

			mCtx = new MongoContext();

			InitControllers();
		}

		void InitControllers()
		{
			treeView.SuspendLayout();

			ClearControllers();
			bAPISubj = false;
			
			{ //Производители
				GenericMongoController<Manufacturer, int> ctl = new GenericMongoController<Manufacturer, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["name"] = new ListLambdaBinding<Manufacturer, object>("Название", x => x.name);

				var fb = ctl.FormBindings;
				fb["name"] = new FormBinding<MongoContext, Manufacturer, object>("Название", TableForm.ControlTypes.TextBox, m => m.name, (c, m, v) => m.name = (string)v, () => "");

				ctl.ListPredicate = x => true;
				ctl.ListOrder = x => x.name;
				ctl.KeySelector = x => x.id;

				ctl.CustomSave = (f, c, e, b) => {
					if (!b) e.id = MongoCounter.NextValue(e.GetType().Name);
				};

				ctl.SetupDataGrid();
				ctl.SetupForm();

				AddController("manufacturer", "Производители", ctl);
			}

			{ //Атрибуты
				GenericMongoController<Attribute, int> ctl = new GenericMongoController<Attribute, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["name"] = new ListLambdaBinding<Attribute, object>("Название", x => x.name);

				var fb = ctl.FormBindings;
				fb["name"] = new FormBinding<MongoContext, Attribute, object>("Название", TableForm.ControlTypes.TextBox, m => m.name, (c, m, v) => m.name = (string)v, () => "", v => ((string)v).Length>0);

				ctl.ListPredicate = x => true;
				ctl.ListOrder = x => x.name;
				ctl.KeySelector = x => x.id;
				
				ctl.CustomSave = (f, c, e, b) => {
					if (!b) e.id = MongoCounter.NextValue(e.GetType().Name);
				};

				ctl.SetupDataGrid();
				ctl.SetupForm();

				AddController("attribute", "Атрибуты", ctl);
			}

			{ //Категории
				GenericMongoController<Category, int> ctl = new GenericMongoController<Category, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["name"] = new ListLambdaBinding<Category, object>("Название", x => x.name);
				cb["parent"] = new ListLambdaBinding<Category, object>("Родительская категория", 
					x => {
						if (x.parent_id==null) return "(нет)";
						var c = mCtx.Set<Category>().AsQueryable().FirstOrDefault(e => x.parent_id.Value==e.id);
						return c!=null ? c.name : "(нет)";
					});

				var fb = ctl.FormBindings;
				fb["name"] = new FormBinding<MongoContext, Category, object>("Название", TableForm.ControlTypes.TextBox, m => m.name, (c, m, v) => m.name = (string)v, () => "");
				fb["parent"] = new FormBinding<MongoContext, Category, object>("Родительская", TableForm.ControlTypes.ComboBox, m => m.parent_id, (c, m, v) => m.parent_id = ((int?)v!=-1 ? (int?)v : null), () => null);

				ctl.ListPredicate = x => true;
				ctl.ListOrder = x => x.name;
				ctl.KeySelector = x => x.id;
				
				ctl.CustomSave = (f, c, e, b) => {
					if (!b) e.id = MongoCounter.NextValue(e.GetType().Name);
				};

				AddCBRefreshHandlers(ctl);

				ctl.SetupDataGrid();
				ctl.SetupForm();

				var ds = new ComboBoxFilteredMongoDataSource< Category, int>(ctl.Form.GetControl("parent") as ComboBox, x => x.id, x => x.name, 300.0d, true);
				ds.NullKey = -1;

				AddController("category", "Категории", ctl);
			}

			{ //Заказы
				GenericMongoController<Order, int> ctl = new GenericMongoController<Order, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["customer"] = new ListLambdaBinding<Order, object>("Покупатель", x => x.last_name+" "+x.first_name+" "+x.middle_name);
				cb["date"] = new ListLambdaBinding<Order, object>("Дата", x => x.date.ToShortDateString());
				cb["total"] = new ListLambdaBinding<Order, object>("Сумма заказа", x => x.products.Sum(p => p.price*p.quantity));

				var fb = ctl.FormBindings;
				fb["date"] = new FormBinding<MongoContext, Order, object>("Дата", TableForm.ControlTypes.DateTimePicker, e => e.date, (c, e, v) => e.date = (DateTime)v, () => DateTime.Today);
				fb["customer"] = new FormBinding<MongoContext, Order, object>("Покупатель", TableForm.ControlTypes.ComboBox, m => m.customer_id, 
					null, () => null, x => x!=null);

				fb["address_id"] = new FormBinding<MongoContext, Order, object>("Адрес доставки", TableForm.ControlTypes.ComboBox, null, 
					null, () => null, x => x!=null && ((int?)x).Value!=-1);

				fb["status"] = new FormBinding<MongoContext, Order, object>("Статус заказа", TableForm.ControlTypes.ComboBox, e => e.status_id, (c, e, v) => e.status_id = (int)v, () => null, x => x!=null && ((int?)x).Value!=-1);

				fb["country"] = new FormBinding<MongoContext, Order, object>("Страна", TableForm.ControlTypes.TextBox, m => m.country, (c, m, v) => m.country = (string)v, () => "", x => x!="");
				fb["city"] = new FormBinding<MongoContext, Order, object>("Город", TableForm.ControlTypes.TextBox, m => m.city, (c, m, v) => m.city = (string)v, () => "", x => x!="");
				fb["address"] = new FormBinding<MongoContext, Order, object>("Адрес", TableForm.ControlTypes.TextBox, m => m.address, (c, m, v) => m.address = (string)v, () => "", x => x!="");
				fb["postcode"] = new FormBinding<MongoContext, Order, object>("Почтовый индекс", TableForm.ControlTypes.TextBox, m => m.postcode, (c, m, v) => m.postcode = (string)v, () => "", x => x!="");

				fb["address_id"].Visiblity = FormBinding<MongoContext, Order, object>.Visibility.Create;
				fb["country"].Visiblity = FormBinding<MongoContext, Order, object>.Visibility.Edit;
				fb["city"].Visiblity = FormBinding<MongoContext, Order, object>.Visibility.Edit;
				fb["address"].Visiblity = FormBinding<MongoContext, Order, object>.Visibility.Edit;
				fb["postcode"].Visiblity = FormBinding<MongoContext, Order, object>.Visibility.Edit;

				ctl.KeySelector = x => x.id;
				ctl.ListPredicate = x => true;
				ctl.ListOrder = x => x.date;
				ctl.OrderAscending = false;
				
				ctl.CustomSave = (f, c, e, b) => {
					if (!b) e.id = MongoCounter.NextValue(e.GetType().Name);
					
					e.customer_id = f.GetValue<int>("customer");
					var m = c.Set<Customer>().AsQueryable().FirstOrDefault(x => x.id==e.customer_id); 
					e.first_name = m.first_name; 
					e.middle_name= m.middle_name; 
					e.last_name=   m.last_name; 
					e.date = DateTime.Now; 
					
					Address ad = m.addresses.FirstOrDefault(x => x.id==f.GetValue<int>("address_id")); 
					e.country = ad.country; 
					e.city = ad.city; 
					e.address = ad.address; 
					e.postcode = ad.postcode;
				};
				
				AddCBRefreshHandlers(ctl);
				

				ctl.SetupDataGrid();
				ctl.SetupForm();

				//Дата
				var dtp = ctl.Form.TableForm.GetControl<DateTimePicker>("date");
				dtp.Format = DateTimePickerFormat.Short;

				// покупатель
				var srccust = new ComboBoxFilteredMongoDataSource< Customer, int>((ComboBox)ctl.Form.GetControl("customer"), e => e.id, e => e.last_name+" "+e.first_name+" "+e.middle_name,300, false, false, e => e.enabled==true);
				srccust.NullKey = -1;

				// его адрес
				var src = new ComboBoxFilteredMongoDataSource< Address, int>((ComboBox)ctl.Form.GetControl("address_id"), e => e.id, e=>e.country+", "+e.city+", "+e.address+", "+e.postcode, 300, false, 
					srcSelector: c => {
					int? v = (int?)ctl.Form.GetValue("customer");
					return v==null ? Enumerable.Empty<Address>().AsQueryable() : c.Set<Customer>().Find(e => e.id==v.Value).First().addresses.AsQueryable();
				});
				src.NullKey = -1;

				//srccust.Refresh();

				((ComboBox)ctl.Form.GetControl("customer")).SelectedValueChanged += (s, e) => {
					ctl.Form.GetControl("address_id").Text = "";
					src.Refresh();
				};

				var srcstatus = new ComboBoxFilteredMongoDataSource< OrderStatus, int>((ComboBox)ctl.Form.GetControl("status"), e => e.id, e => e.name, 300, false);
				//srcstatus.Refresh();

				AddController("order", "Заказы", ctl);

				{ //Товары заказа
					ctl.Form.Tabs.TabPages.Add("product", "Товары");
					ctl.Form.EnableTabs = true;

					GenericMongoController<OrderProduct, int> opctl = new GenericMongoController<OrderProduct, int>(ctl.Form.Tabs.TabPages["product"]);
					opctl.Parent = ctl;
					opctl.ParentCollection = "products";

					var pc = opctl.ColumnBindings;
					pc["product"] = new ListLambdaBinding<OrderProduct, object>("Товар",
						x => {
							var p = mCtx.Set<Product>().Find(e => e.id == x.product_id).FirstOrDefault();
							return p!=null ? p.name : "(нет)";
						});

					pc["price"] = new ListLambdaBinding<OrderProduct, object>("Цена", x => x.price);
					pc["quantity"] = new ListLambdaBinding<OrderProduct, object>("Количество", x => x.quantity);

					var pf = opctl.FormBindings;
					pf["product"] = new FormBinding<MongoContext, OrderProduct, object>("Товар", TableForm.ControlTypes.ComboBox, x => x.product_id, (c, x, v) => x.product_id = (int)v, () => null, x => x!=null);
					pf["quantity"] = new FormBinding<MongoContext, OrderProduct, object>("Количество", TableForm.ControlTypes.NumericUpDown, x => x.quantity, (c, e, v) => e.quantity=(int)(decimal)v, () => 1m, v => ((int)(decimal)v)>0);
					
					opctl.KeySelector = x => x.id;
					opctl.ListPredicate = x => true;
					opctl.ListOrder = x => x.id;				

					opctl.ValidateForm = (f, c, b) => {
						bool valid = true;
						int? prod = (int?)f.GetValue("product");

						valid = prod!=null 
							&& ((decimal)f.GetValue("quantity"))>=1 
							&& (b || !ctl.GetCollection<OrderProduct>("products", null).Any(x => x.product_id==prod.Value));

						return valid;
					};

					opctl.CustomSave = (form, ctx, e, b) => {
						if (!b) e.id = MongoCounter.NextValue(e.GetType().Name);
						e.product_id = (int)form.GetValue("product");
						e.quantity = (int)(decimal)form.GetValue("quantity");
						e.price = ctx.Set<Product>().AsQueryable().First(x => x.id==e.product_id).price;
					};
					
					AddCBRefreshHandlers(opctl);

					opctl.SetupDataGrid();
					opctl.SetupForm();

					NumericUpDown nud = opctl.Form.GetControl("quantity") as NumericUpDown;
					nud.Minimum = 1;
					nud.Maximum = 10000;
					nud.DecimalPlaces = 0;

					new ComboBoxFilteredMongoDataSource< Product, int>(opctl.Form.GetControl("product") as ComboBox, x => x.id, x => x.name, 300.0, false);
				}
			}

			{ //Товары
				GenericMongoController<Product, int> ctl = new GenericMongoController<Product, int>(splitCont.Panel2);

				var b = ctl.ColumnBindings;
				b["name"] = new ListLambdaBinding<Product, object>("Наименование", x => x.name);
				b["model"] = new ListLambdaBinding<Product, object>("Модель", x => x.model);
				b["manufacturer"] = new ListLambdaBinding<Product, object>("Производитель", 
					x => {
						var p = mCtx.Set<Manufacturer>().Find(e => e.id == x.manufacturer_id).FirstOrDefault();
						return p!=null ? p.name : "(нет)";
					});

				var f = ctl.FormBindings;
				f["name"] = new FormBinding<MongoContext, Product, object>("Наименование", TableForm.ControlTypes.TextBox, p => p.name, (c, p, v) => p.name = (string)v, () => "");
				f["model"] = new FormBinding<MongoContext, Product, object>("Модель", TableForm.ControlTypes.TextBox, p => p.model, (c, p, v) => p.model = (string)v, () => "");
				f["description"] = new FormBinding<MongoContext, Product, object>("Описание", TableForm.ControlTypes.TextBox, p => p.description, (c, p, v) => p.description = (string)v, () => "");
				f["price"] = new FormBinding<MongoContext, Product, object>("Цена", TableForm.ControlTypes.NumericUpDown, p => p.price, (c, p, v) => p.price = (decimal)v, () => 0.0m);
				f["manufacturer"] = new FormBinding<MongoContext, Product, object>("Производитель", TableForm.ControlTypes.ComboBox, p => p.manufacturer_id, (c, p, v) => p.manufacturer_id = (int)v, () => null, x => x!=null);
				f["category"] = new FormBinding<MongoContext, Product, object>("Категория", TableForm.ControlTypes.ComboBox, p => p.category_id, (c, p, v) => p.category_id = (int)v, () => null, x => x!=null);
				f["status"] = new FormBinding<MongoContext, Product, object>("Статус", TableForm.ControlTypes.ComboBox, p => p.status_id, (c, p, v) => p.status_id = (int)v, () => null, x => x!=null);
				
				ctl.KeySelector = x => x.id;
				ctl.ListPredicate = x => true;
				ctl.ListOrder = x => x.name;
				
				ctl.CustomSave = (fo, c, e, bo) => {
					if (!bo) e.id = MongoCounter.NextValue(e.GetType().Name);
				};
				
				AddCBRefreshHandlers(ctl);

				ctl.SetupDataGrid();
				ctl.SetupForm();

				NumericUpDown nud = (ctl.Form.GetControl("price") as NumericUpDown);
				nud.DecimalPlaces = 2;
				nud.Minimum = 0;
				nud.Maximum = 1000000;

				new ComboBoxFilteredMongoDataSource< Manufacturer, int>(ctl.Form.GetControl("manufacturer") as ComboBox, x => x.id, x => x.name, 300, false);
				new ComboBoxFilteredMongoDataSource< Category, int>(ctl.Form.GetControl("category") as ComboBox, x => x.id, x => x.name, 300, false);
				new ComboBoxFilteredMongoDataSource< ProductStatus, int>(ctl.Form.GetControl("status") as ComboBox, x => x.id, x => x.name, 300, false);

				AddController("product", "Товары", ctl);

				{ //Атрибуты товара
					ctl.Form.Tabs.TabPages.Add("attribute", "Атрибуты");
					ctl.Form.EnableTabs = true;

					GenericMongoController<ProductAttribute, int> atctl = new GenericMongoController<ProductAttribute, int>(ctl.Form.Tabs.TabPages["attribute"]);
					atctl.Parent = ctl;
					atctl.ParentCollection = "attributes";

					var cb = atctl.ColumnBindings;
					cb["name"] = new ListLambdaBinding<ProductAttribute, object>("Атрибут", 
						x => {
							var p = mCtx.Set<Attribute>().Find(e => e.id == x.attribute_id).FirstOrDefault();
							return p!=null ? p.name : "(нет)";
						});

					cb["value"] = new ListLambdaBinding<ProductAttribute, object>("Значение", e => e.value);

					var fb = atctl.FormBindings;
					fb["name"] = new FormBinding<MongoContext,ProductAttribute,object>("Атрибут", TableForm.ControlTypes.ComboBox, e => e.attribute_id, (c,e,v) => e.attribute_id=(int)v, () => null, v => v!=null);
					fb["value"] = new FormBinding<MongoContext,ProductAttribute,object>("Значение", TableForm.ControlTypes.TextBox, e => e.value, (c,e,v) => e.value=(string)v, () => "");
					
					atctl.KeySelector = x => x.id;
					atctl.ListPredicate = e => true;
					atctl.ListOrder = e => e.id;
				
					atctl.CustomSave = (fo, c, e, bo) => {
						if (!bo) e.id = MongoCounter.NextValue(e.GetType().Name);
					};
					
					AddCBRefreshHandlers(atctl);

					atctl.SetupDataGrid();
					atctl.SetupForm();

					new ComboBoxFilteredMongoDataSource< Attribute, int>((ComboBox)atctl.Form.GetControl("name"), e=>e.id, e=>e.name, 300, false);
				}
			}

			{ //Статус товара
				GenericMongoController<ProductStatus, int> ctl = new GenericMongoController<ProductStatus, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["name"] = new ListLambdaBinding<ProductStatus, object>("Описание", x => x.name);

				var fb = ctl.FormBindings;
				fb["name"] = new FormBinding<MongoContext, ProductStatus, object>("Описание", TableForm.ControlTypes.TextBox, m => m.name, (c, m, v) => m.name = (string)v, () => "");
				
				ctl.KeySelector = x => x.id;
				ctl.ListPredicate = x => true;
				ctl.ListOrder = x => x.name;
				
				ctl.CustomSave = (f, c, e, b) => {
					if (!b) e.id = MongoCounter.NextValue(e.GetType().Name);
				};

				ctl.SetupDataGrid();
				ctl.SetupForm();
				
				AddController("product_status", "Статусы товаров", ctl);
			}

			{ //Статус заказа
				GenericMongoController<OrderStatus, int> ctl = new GenericMongoController<OrderStatus, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["name"] = new ListLambdaBinding<OrderStatus, object>("Описание", x => x.name);

				var fb = ctl.FormBindings;
				fb["name"] = new FormBinding<MongoContext, OrderStatus, object>("Описание", TableForm.ControlTypes.TextBox, m => m.name, (c, m, v) => m.name = (string)v, () => "");
				
				ctl.KeySelector = x => x.id;
				ctl.ListPredicate = x => true;
				ctl.ListOrder = x => x.name;
				
				ctl.CustomSave = (f, c, e, b) => {
					if (!b) e.id = MongoCounter.NextValue(e.GetType().Name);
				};

				ctl.SetupDataGrid();
				ctl.SetupForm();

				AddController("order_status", "Статусы заказов", ctl);
			}

			{ //Покупатель
				GenericMongoController<Customer, int> ctl = new GenericMongoController<Customer, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["login"] = new ListLambdaBinding<Customer,object>("Логин", e => e.login);
				cb["name"] = new ListLambdaBinding<Customer,object>("ФИО", e => string.Format("{0} {1} {2}", e.last_name, e.first_name, e.middle_name));
				cb["phone"] = new ListLambdaBinding<Customer,object>("Телефон", e => e.phone);
				cb["email"] = new ListLambdaBinding<Customer,object>("Email", e => e.email);

				var fb = ctl.FormBindings;
				fb["login"] = new FormBinding<MongoContext, Customer, object>("Логин", TableForm.ControlTypes.TextBox, e => e.login, (c, e, v) => { e.login=(string)v; e.enabled=true; }, () => "", v => ((string)v).Length>0);
				//fb["password"] = new FormBinding<MongoContext,Customer,object>("Пароль", TableForm.ControlTypes.TextBox, e => "", (c,e,v) => e.password=(string)v, () => "", v => ((string)v).Length>0);
				fb["last_name"] = new FormBinding<MongoContext,Customer,object>("Фамилия", TableForm.ControlTypes.TextBox, e => e.last_name, (c,e,v) => e.last_name=(string)v, () => "");
				fb["first_name"] = new FormBinding<MongoContext,Customer,object>("Имя", TableForm.ControlTypes.TextBox, e => e.first_name, (c,e,v) => e.first_name=(string)v, () => "");
				fb["middle_name"] = new FormBinding<MongoContext,Customer,object>("Отчество", TableForm.ControlTypes.TextBox, e => e.middle_name, (c,e,v) => e.middle_name=(string)v, () => "");
				fb["phone"] = new FormBinding<MongoContext,Customer,object>("Телефон", TableForm.ControlTypes.TextBox, e => e.phone, (c,e,v) => e.phone=(string)v, () => "", v => System.Text.RegularExpressions.Regex.Match((string)v, @"^(\+?\d+)?$").Success);
				fb["email"] = new FormBinding<MongoContext,Customer,object>("Email", TableForm.ControlTypes.TextBox, e => e.email, (c,e,v) => e.email=(string)v, () => "", v => System.Text.RegularExpressions.Regex.Match((string)v, @"^(.+@.+)?$").Success);
				
				ctl.KeySelector = x => x.id;
				ctl.ListPredicate = x => x.enabled!=null;
				ctl.ListOrders.AddRange(new Expression<Func<Customer, object>>[]{x => x.last_name, x => x.first_name, x => x.middle_name});
				
				ctl.ValidateForm = (fo, c, bo) => {
					string login = fo.GetValue<string>("login");

					bool valid = login.Length>0
						//&& fo.GetValue<string>("password").Length>0
						&& System.Text.RegularExpressions.Regex.Match(fo.GetValue<string>("phone"), @"^(\+?\d+)?$").Success
						&& System.Text.RegularExpressions.Regex.Match(fo.GetValue<string>("email"), @"^(.+@.+)?$").Success;

					// Проверяем, есть ли уже такое имя
					valid = valid && (bo || !c.Set<Customer>().AsQueryable().Any(x => x.login==login));

					return valid;
				};
				
				ctl.CustomSave = (f, c, e, b) => {
					if (!b) e.id = MongoCounter.NextValue(e.GetType().Name);
				};

				ctl.SetupDataGrid();
				ctl.SetupForm();

				//((TextBox)ctl.Form.GetControl("password")).UseSystemPasswordChar = true;

				AddController("customer", "Покупатели", ctl);

				{ //Адрес покупателя
					ctl.Form.Tabs.TabPages.Add("address", "Адреса");
					ctl.Form.EnableTabs = true;

					GenericMongoController<Address, int> atctl = new GenericMongoController<Address, int>(ctl.Form.Tabs.TabPages["address"]);
					atctl.Parent = ctl;
					atctl.ParentCollection = "addresses";

					var cb2 = atctl.ColumnBindings;
					cb2["country"] = new ListLambdaBinding<Address, object>("Страна", e => e.country);
					cb2["city"] = new ListLambdaBinding<Address, object>("Населённый пункт", e => e.city);
					cb2["address"] = new ListLambdaBinding<Address, object>("Адрес", e => e.address);
					cb2["postcode"] = new ListLambdaBinding<Address, object>("Почтовый индекс", e => e.postcode);

					var fb2 = atctl.FormBindings;
					fb2["country"] = new FormBinding<MongoContext,Address,object>("Страна", TableForm.ControlTypes.TextBox, e => e.country, (c,e,v) => e.country=(string)v, () => "", v => ""!=(string)v);
					fb2["city"] = new FormBinding<MongoContext,Address,object>("Город/насёлённый пункт", TableForm.ControlTypes.TextBox, e => e.city, (c,e,v) => e.city=(string)v, () => "");
					fb2["address"] = new FormBinding<MongoContext,Address,object>("Адрес", TableForm.ControlTypes.TextBox, e => e.address, (c,e,v) => e.address=(string)v, () => "", v => ""!=(string)v);
					fb2["postcode"] = new FormBinding<MongoContext,Address,object>("Почтовый индекс", TableForm.ControlTypes.TextBox, e => e.postcode, (c,e,v) => e.postcode=(string)v, () => "");
					
					atctl.KeySelector = x => x.id;
					atctl.ListPredicate = e => true;
					atctl.ListOrder = e => e.id;
				
					atctl.CustomSave = (f, c, e, b) => {
						if (!b) e.id = MongoCounter.NextValue(e.GetType().Name);
					};	

					atctl.SetupDataGrid();
					atctl.SetupForm();
				}
			}

			treeView.ResumeLayout();
		}

		void AddController(string name, string text, IController ctl)
		{
			ctrls.Add(name, ctl);
			treeView.Nodes.Add(name, text);
		}

		void ClearControllers()
		{
			ctrls.Clear();
			treeView.Nodes.Clear();
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			IController c = ctrls[treeView.SelectedNode.Name];
			if (c!=null) c.Show();
		}

		private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new ExportForm().ShowDialog();
		}

		private void заказыToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Task.Run(() => Filler.CreateOrders(100000, 500));
		}

		private void tsmChangeSubject_Click(object sender, EventArgs e)
		{
			if (bAPISubj) {
				InitControllers();
			} else {
				InitControllersAPI();
			}
		}

		private void установитьБДToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mCtx.Set<Order>().Indexes.CreateOne(Builders<Order>.IndexKeys.Descending(x => x.date));
			mCtx.Set<Product>().Indexes.CreateOne(Builders<Product>.IndexKeys.Ascending(x => x.name));
			mCtx.Set<ProductStatus>().Indexes.CreateOne(Builders<ProductStatus>.IndexKeys.Ascending(x => x.name));
			mCtx.Set<OrderStatus>().Indexes.CreateOne(Builders<OrderStatus>.IndexKeys.Ascending(x => x.name));
			mCtx.Set<Attribute>().Indexes.CreateOne(Builders<Attribute>.IndexKeys.Ascending(x => x.name));
			mCtx.Set<Manufacturer>().Indexes.CreateOne(Builders<Manufacturer>.IndexKeys.Ascending(x => x.name));
			mCtx.Set<Customer>().Indexes.CreateOne(Builders<Customer>.IndexKeys.Ascending(x => x.last_name).Ascending(x => x.first_name).Ascending(x => x.middle_name));
		}
	}
}
