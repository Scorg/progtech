using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.ServiceModel;
using Npgsql;
using NpgsqlTypes;
using common;
using System.Web;
using tp_lab.misha_api;

namespace tp_lab {
	public partial class MainForm : Form {
		Dictionary<string, Controller> ctrls;
		bool bAPISubj;

		public MainForm()
		{
			InitializeComponent();
			ctrls = new Dictionary<string, Controller>();

			InitControllers();
		}

		void InitControllers()
		{
			treeView.SuspendLayout();

			ClearControllers();
			bAPISubj = false;

			{ //Производители
				GenericController<Entities, Manufacturer> ctl = new GenericController<Entities, Manufacturer>(splitCont.Panel2);

				var b = ctl.ColumnBindings;
				b["name"] = new ListLambdaBinding<Manufacturer, object>("Название", x => x.name);

				var f = ctl.FormBindings;
				f["name"] = new FormBinding<Entities, Manufacturer, object>("Название", TableForm.ControlTypes.TextBox, m => m.name, (c, m, v) => m.name = (string)v, () => "");

				ctl.ListPredicate = x => true;
				ctl.ListOrder = x => x.name;

				ctl.SetupDataGrid();
				ctl.SetupForm();

				AddController("manufacturer", "Производители", ctl);
			}

			{ //Атрибуты
				GenericController<Entities, Attribute> ctl = new GenericController<Entities, Attribute>(splitCont.Panel2);

				var b = ctl.ColumnBindings;
				b["name"] = new ListLambdaBinding<Attribute, object>("Название", x => x.name);

				var f = ctl.FormBindings;
				f["name"] = new FormBinding<Entities, Attribute, object>("Название", TableForm.ControlTypes.TextBox, m => m.name, (c, m, v) => m.name = (string)v, () => "", v => ((string)v).Length>0);

				ctl.ListPredicate = x => true;
				ctl.ListOrder = x => x.name;

				ctl.SetupDataGrid();
				ctl.SetupForm();

				AddController("attribute", "Атрибуты", ctl);
			}

			{ //Категории
				GenericController<Entities, Category> ctl = new GenericController<Entities, Category>(splitCont.Panel2);

				var b = ctl.ColumnBindings;
				b["name"] = new ListLambdaBinding<Category, object>("Название", x => x.name);
				b["parent"] = new ListLambdaBinding<Category, object>("Родительская категория", x => x.parent_category!=null ? x.parent_category.name : "");

				var f = ctl.FormBindings;
				f["name"] = new FormBinding<Entities, Category, object>("Название", TableForm.ControlTypes.TextBox, m => m.name, (c, m, v) => m.name = (string)v, () => "");
				f["parent"] = new FormBinding<Entities, Category, object>("Родительская", TableForm.ControlTypes.ComboBox, m => m.parent_id, (c, m, v) => m.parent_id = (v!=null && ((int?)v).Value!=default(int) ? (int?)v : null), () => null);

				ctl.ListPredicate = x => true;
				ctl.ListOrder = x => x.name;

				ctl.SetupDataGrid();
				ctl.SetupForm();

				var ds = new ComboBoxFilteredDataSource<Entities, Category, int>(ctl.Form.GetControl("parent") as ComboBox, x => x.id, x => x.name, 300.0d, true);
				//ds.NullKey = -1;

				AddController("category", "Категории", ctl);
			}

			{ //Заказы
				GenericController<Entities, Order> ctl = new GenericController<Entities, Order>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["customer"] = new ListLambdaBinding<Order, object>("Покупатель", x => x.last_name+" "+x.first_name+" "+x.middle_name);
				cb["date"] = new ListLambdaBinding<Order, object>("Дата", x => x.date.ToShortDateString());
				cb["total"] = new ListLambdaBinding<Order, object>("Сумма заказа", x => x.products.Sum(p => p.price*p.quantity));

				var fb = ctl.FormBindings;
				fb["customer"] = new FormBinding<Entities, Order, object>("Покупатель", TableForm.ControlTypes.ComboBox, m => m.customer_id, (c, m, v) => { m.customer = c.customer.Find(new object[] { v }); m.first_name = m.customer.first_name; m.middle_name=m.customer.middle_name; m.last_name=m.customer.last_name; m.date = DateTime.Now; }, () => null, x => x!=null);
				fb["address_id"] = new FormBinding<Entities, Order, object>("Адрес доставки", TableForm.ControlTypes.ComboBox, null, (c, e, v) => 
				{ 
					Address ad = c.address.Find(new object[] { v }); 
					e.country = ad.country; 
					e.city = ad.city; 
					e.address = ad.address; 
					e.postcode = ad.postcode; 
				}, () => null, x => x!=null);
				fb["status"] = new FormBinding<Entities, Order, object>("Статус заказа", TableForm.ControlTypes.ComboBox, e => e.status_id, (c, e, v) => e.status_id = (int)v, () => null, x => x!=null);

				fb["country"] = new FormBinding<Entities, Order, object>("Страна", TableForm.ControlTypes.TextBox, m => m.country, (c, m, v) => m.country = (string)v, () => "", x => x!="");
				fb["city"] = new FormBinding<Entities, Order, object>("Город", TableForm.ControlTypes.TextBox, m => m.city, (c, m, v) => m.city = (string)v, () => "", x => x!="");
				fb["address"] = new FormBinding<Entities, Order, object>("Адрес", TableForm.ControlTypes.TextBox, m => m.address, (c, m, v) => m.address = (string)v, () => "", x => x!="");
				fb["postcode"] = new FormBinding<Entities, Order, object>("Почтовый индекс", TableForm.ControlTypes.TextBox, m => m.postcode, (c, m, v) => m.postcode = (string)v, () => "", x => x!="");

				fb["address_id"].Visiblity = FormBinding<Entities, Order, object>.Visibility.Create;
				fb["country"].Visiblity = FormBinding<Entities, Order, object>.Visibility.Edit;
				fb["city"].Visiblity = FormBinding<Entities, Order, object>.Visibility.Edit;
				fb["address"].Visiblity = FormBinding<Entities, Order, object>.Visibility.Edit;
				fb["postcode"].Visiblity = FormBinding<Entities, Order, object>.Visibility.Edit;

				ctl.ListPredicate = x => true;
				ctl.ListOrder = x => new { x.date };
				ctl.OrderAscending = false;

				ctl.SetupDataGrid();
				ctl.SetupForm();

				// покупатель
				var srccust = new ComboBoxFilteredDataSource<Entities, Customer, int>((ComboBox)ctl.Form.GetControl("customer"), e => e.id, e => e.last_name+" "+e.first_name+" "+e.middle_name,300, false, false, e => e.enabled==true);

				// его адрес
				var src = new ComboBoxFilteredDataSource<Entities, Address, int>((ComboBox)ctl.Form.GetControl("address_id"), e => e.id, e=>e.country+", "+e.city+", "+e.address+", "+e.postcode, 300, false, 
					srcSelector: c => {
					int? v = (int?)ctl.Form.GetValue("customer");
					return v==null ? Enumerable.Empty<Address>().AsQueryable() : c.address.Where(e => e.customer_id==v.Value);
				});

				//srccust.Refresh();

				((ComboBox)ctl.Form.GetControl("customer")).SelectedValueChanged += (s, e) => {
					ctl.Form.GetControl("address_id").Text = "";
					src.Refresh();
				};

				var srcstatus = new ComboBoxFilteredDataSource<Entities, OrderStatus, int>((ComboBox)ctl.Form.GetControl("status"), e => e.id, e => e.name, 300, false);
				//srcstatus.Refresh();

				AddController("order", "Заказы", ctl);

				{ //Товары заказа
					ctl.Form.Tabs.TabPages.Add("product", "Товары");
					ctl.Form.EnableTabs = true;

					GenericController<Entities, OrderProduct> opctl = new GenericController<Entities, OrderProduct>(ctl.Form.Tabs.TabPages["product"]);
					opctl.Parent = ctl;
					opctl.ParentCollection = "products";

					var pc = opctl.ColumnBindings;
					pc["product"] = new ListLambdaBinding<OrderProduct, object>("Товар", x => x.product.name);
					pc["price"] = new ListLambdaBinding<OrderProduct, object>("Цена", x => x.price);
					pc["quantity"] = new ListLambdaBinding<OrderProduct, object>("Количество", x => x.quantity);

					var pf = opctl.FormBindings;
					pf["product"] = new FormBinding<Entities, OrderProduct, object>("Товар", TableForm.ControlTypes.ComboBox, x => x.product_id, (c, x, v) => x.product_id = (int)v, () => null, x => x!=null);
					pf["quantity"] = new FormBinding<Entities, OrderProduct, object>("Количество", TableForm.ControlTypes.NumericUpDown, x => x.quantity, (c, e, v) => e.quantity=(int)(decimal)v, () => 1m, v => ((int)(decimal)v)>0);

					opctl.ListPredicate = x => true;
					opctl.ListOrder = x => x.product.name;

					opctl.ValidateForm = (f, c, b) => {
						bool valid = true;
						int? prod = (int?)f.GetValue("product");

						valid = prod!=null 
							&& ((decimal)f.GetValue("quantity"))>=1 
							&& (b || !ctl.GetCollection<OrderProduct>("products", null).Any(x => x.product_id==prod.Value));

						return valid;
					};

					opctl.CustomSave = (form, ctx, e) => {
						e.product_id = (int)form.GetValue("product");
						e.quantity = (int)(decimal)form.GetValue("quantity");
						e.price = ctx.Set<Product>().First(x => x.id==e.product_id).price;
					};

					opctl.SetupDataGrid();
					opctl.SetupForm();

					NumericUpDown nud = opctl.Form.GetControl("quantity") as NumericUpDown;
					nud.Minimum = 1;
					nud.Maximum = 10000;
					nud.DecimalPlaces = 0;

					new ComboBoxFilteredDataSource<Entities, Product, int>(opctl.Form.GetControl("product") as ComboBox, x => x.id, x => x.name, 300.0, false);
				}
			}

			{ //Товары
				GenericController<Entities, Product> ctl = new GenericController<Entities, Product>(splitCont.Panel2);

				var b = ctl.ColumnBindings;
				b["name"] = new ListLambdaBinding<Product, object>("Наименование", x => x.name);
				b["model"] = new ListLambdaBinding<Product, object>("Модель", x => x.model);
				b["manufacturer"] = new ListLambdaBinding<Product, object>("Производитель", x => x.manufacturer.name);

				var f = ctl.FormBindings;
				f["name"] = new FormBinding<Entities, Product, object>("Наименование", TableForm.ControlTypes.TextBox, p => p.name, (c, p, v) => p.name = (string)v, () => "");
				f["model"] = new FormBinding<Entities, Product, object>("Модель", TableForm.ControlTypes.TextBox, p => p.model, (c, p, v) => p.model = (string)v, () => "");
				f["description"] = new FormBinding<Entities, Product, object>("Описание", TableForm.ControlTypes.TextBox, p => p.description, (c, p, v) => p.description = (string)v, () => "");
				f["price"] = new FormBinding<Entities, Product, object>("Цена", TableForm.ControlTypes.NumericUpDown, p => p.price, (c, p, v) => p.price = (decimal)v, () => 0.0m);
				f["manufacturer"] = new FormBinding<Entities, Product, object>("Производитель", TableForm.ControlTypes.ComboBox, p => p.manufacturer_id, (c, p, v) => p.manufacturer_id = (int)v, () => null, x => x!=null);
				f["category"] = new FormBinding<Entities, Product, object>("Категория", TableForm.ControlTypes.ComboBox, p => p.category_id, (c, p, v) => p.category_id = (int)v, () => null, x => x!=null);
				f["status"] = new FormBinding<Entities, Product, object>("Статус", TableForm.ControlTypes.ComboBox, p => p.status_id, (c, p, v) => p.status_id = (int)v, () => null, x => x!=null);

				ctl.ListPredicate = x => true;
				ctl.ListOrder = x => x.name;

				ctl.SetupDataGrid();
				ctl.SetupForm();

				NumericUpDown nud = (ctl.Form.GetControl("price") as NumericUpDown);
				nud.DecimalPlaces = 2;
				nud.Minimum = 0;
				nud.Maximum = 1000000;

				new ComboBoxFilteredDataSource<Entities, Manufacturer, int>(ctl.Form.GetControl("manufacturer") as ComboBox, x => x.id, x => x.name, 300, false);
				new ComboBoxFilteredDataSource<Entities, Category, int>(ctl.Form.GetControl("category") as ComboBox, x => x.id, x => x.name, 300, false);
				new ComboBoxFilteredDataSource<Entities, ProductStatus, int>(ctl.Form.GetControl("status") as ComboBox, x => x.id, x => x.name, 300, false);

				AddController("product", "Товары", ctl);

				{ //Атрибуты товара
					ctl.Form.Tabs.TabPages.Add("attribute", "Атрибуты");
					ctl.Form.EnableTabs = true;

					GenericController<Entities, ProductAttribute> atctl = new GenericController<Entities, ProductAttribute>(ctl.Form.Tabs.TabPages["attribute"]);
					atctl.Parent = ctl;
					atctl.ParentCollection = "attributes";

					var cb = atctl.ColumnBindings;
					cb["name"] = new ListLambdaBinding<ProductAttribute, object>("Атрибут", e => e.attribute.name);
					cb["value"] = new ListLambdaBinding<ProductAttribute, object>("Значение", e => e.value);

					var fb = atctl.FormBindings;
					fb["name"] = new FormBinding<Entities,ProductAttribute,object>("Атрибут", TableForm.ControlTypes.ComboBox, e => e.attribute_id, (c,e,v) => e.attribute_id=(int)v, () => null, v => v!=null);
					fb["value"] = new FormBinding<Entities,ProductAttribute,object>("Значение", TableForm.ControlTypes.TextBox, e => e.value, (c,e,v) => e.value=(string)v, () => "");

					atctl.ListPredicate = e => true;
					atctl.ListOrder = e => e.attribute.name;

					atctl.SetupDataGrid();
					atctl.SetupForm();

					new ComboBoxFilteredDataSource<Entities, Attribute, int>((ComboBox)atctl.Form.GetControl("name"), e=>e.id, e=>e.name, 300, false);
				}
			}

			{ //Статус товара
				GenericController<Entities, ProductStatus> ctl = new GenericController<Entities, ProductStatus>(splitCont.Panel2);

				var b = ctl.ColumnBindings;
				b["name"] = new ListLambdaBinding<ProductStatus, object>("Описание", x => x.name);

				var f = ctl.FormBindings;
				f["name"] = new FormBinding<Entities, ProductStatus, object>("Описание", TableForm.ControlTypes.TextBox, m => m.name, (c, m, v) => m.name = (string)v, () => "");

				ctl.ListPredicate = x => true;
				ctl.ListOrder = x => x.name;

				ctl.SetupDataGrid();
				ctl.SetupForm();
				
				AddController("product_status", "Статусы товаров", ctl);
			}

			{ //Статус заказа
				GenericController<Entities, OrderStatus> ctl = new GenericController<Entities, OrderStatus>(splitCont.Panel2);

				var b = ctl.ColumnBindings;
				b["name"] = new ListLambdaBinding<OrderStatus, object>("Описание", x => x.name);

				var f = ctl.FormBindings;
				f["name"] = new FormBinding<Entities, OrderStatus, object>("Описание", TableForm.ControlTypes.TextBox, m => m.name, (c, m, v) => m.name = (string)v, () => "");

				ctl.ListPredicate = x => true;
				ctl.ListOrder = x => x.name;

				ctl.SetupDataGrid();
				ctl.SetupForm();

				AddController("order_status", "Статусы заказов", ctl);
			}

			{ //Покупатель
				GenericController<Entities, Customer> ctl = new GenericController<Entities, Customer>(splitCont.Panel2);

				var b = ctl.ColumnBindings;
				b["login"] = new ListLambdaBinding<Customer,object>("Логин", e => e.login);
				b["name"] = new ListLambdaBinding<Customer,object>("ФИО", e => string.Format("{0} {1} {2}", e.last_name, e.first_name, e.middle_name));
				b["phone"] = new ListLambdaBinding<Customer,object>("Телефон", e => e.phone);
				b["email"] = new ListLambdaBinding<Customer,object>("Email", e => e.email);

				var f = ctl.FormBindings;
				f["login"] = new FormBinding<Entities, Customer, object>("Логин", TableForm.ControlTypes.TextBox, e => e.login, (c, e, v) => { e.login=(string)v; e.enabled=true; }, () => "", v => ((string)v).Length>0);
				f["password"] = new FormBinding<Entities,Customer,object>("Пароль", TableForm.ControlTypes.TextBox, e => "", (c,e,v) => e.password=(string)v, () => "", v => ((string)v).Length>0);
				f["last_name"] = new FormBinding<Entities,Customer,object>("Фамилия", TableForm.ControlTypes.TextBox, e => e.last_name, (c,e,v) => e.last_name=(string)v, () => "");
				f["first_name"] = new FormBinding<Entities,Customer,object>("Имя", TableForm.ControlTypes.TextBox, e => e.first_name, (c,e,v) => e.first_name=(string)v, () => "");
				f["middle_name"] = new FormBinding<Entities,Customer,object>("Отчество", TableForm.ControlTypes.TextBox, e => e.middle_name, (c,e,v) => e.middle_name=(string)v, () => "");
				f["phone"] = new FormBinding<Entities,Customer,object>("Телефон", TableForm.ControlTypes.TextBox, e => e.phone, (c,e,v) => e.phone=(string)v, () => "", v => System.Text.RegularExpressions.Regex.Match((string)v, @"^(\+?\d+)?$").Success);
				f["email"] = new FormBinding<Entities,Customer,object>("Email", TableForm.ControlTypes.TextBox, e => e.email, (c,e,v) => e.email=(string)v, () => "", v => System.Text.RegularExpressions.Regex.Match((string)v, @"^(.+@.+)?$").Success);

				ctl.ListPredicate = x => x.enabled!=null;
				ctl.ListOrder = x => x.last_name+x.first_name+x.middle_name;
				
				ctl.ValidateForm = (fo, c, bo) => {
					string login = fo.GetValue<string>("login");

					bool valid = login.Length>0
						&& fo.GetValue<string>("password").Length>0
						&& System.Text.RegularExpressions.Regex.Match(fo.GetValue<string>("phone"), @"^(\+?\d+)?$").Success
						&& System.Text.RegularExpressions.Regex.Match(fo.GetValue<string>("email"), @"^(.+@.+)?$").Success;

					// Проверяем, есть ли уже такое имя
					valid = valid && (bo || !c.Set<Customer>().Any(x => x.login==login));

					return valid;
				};

				ctl.SetupDataGrid();
				ctl.SetupForm();

				((TextBox)ctl.Form.GetControl("password")).UseSystemPasswordChar = true;

				AddController("customer", "Покупатели", ctl);

				{ //Адрес покупателя
					ctl.Form.Tabs.TabPages.Add("address", "Адреса");
					ctl.Form.EnableTabs = true;

					GenericController<Entities, Address> atctl = new GenericController<Entities, Address>(ctl.Form.Tabs.TabPages["address"]);
					atctl.Parent = ctl;
					atctl.ParentCollection = "addresses";

					var cb = atctl.ColumnBindings;
					cb["country"] = new ListLambdaBinding<Address, object>("Страна", e => e.country);
					cb["city"] = new ListLambdaBinding<Address, object>("Населённый пункт", e => e.city);
					cb["address"] = new ListLambdaBinding<Address, object>("Адрес", e => e.address);
					cb["postcode"] = new ListLambdaBinding<Address, object>("Почтовый индекс", e => e.postcode);

					var fb = atctl.FormBindings;
					fb["country"] = new FormBinding<Entities,Address,object>("Страна", TableForm.ControlTypes.TextBox, e => e.country, (c,e,v) => e.country=(string)v, () => "", v => ""!=(string)v);
					fb["city"] = new FormBinding<Entities,Address,object>("Город/насёлённый пункт", TableForm.ControlTypes.TextBox, e => e.city, (c,e,v) => e.city=(string)v, () => "");
					fb["address"] = new FormBinding<Entities,Address,object>("Адрес", TableForm.ControlTypes.TextBox, e => e.address, (c,e,v) => e.address=(string)v, () => "", v => ""!=(string)v);
					fb["postcode"] = new FormBinding<Entities,Address,object>("Почтовый индекс", TableForm.ControlTypes.TextBox, e => e.postcode, (c,e,v) => e.postcode=(string)v, () => "");

					atctl.ListPredicate = e => true;
					atctl.ListOrder = e => e.id;

					atctl.SetupDataGrid();
					atctl.SetupForm();
				}
			}

			treeView.ResumeLayout();
		}

		void AddController(string name, string text, Controller ctl)
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
			Controller c = ctrls[treeView.SelectedNode.Name];
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
	}
}
