using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using common;
using tp_lab.misha_api;
using tp_lab.misha_api.types;

namespace tp_lab {
	partial class MainForm : Form {
		
		void InitControllersAPI()
		{
			treeView.SuspendLayout();
			ClearControllers();
			bAPISubj = true;

			{ //Торговцы
				var ctl = new GenericAPIController<Dealer, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["name"] = new ListLambdaBinding<Dealer, object>("Название", x => x.name);

				var fb = ctl.FormBindings;
				fb["name"] = new FormAPIBinding<Dealer,object>("Название", TableForm.ControlTypes.TextBox, x => x.name, (e, v) => e.name=(string)v, () => "");

				ctl.SetupDataGrid();
				ctl.SetupForm();

				ctl.KeySelector = x => x.id;

				AddDefaultCRUDHandlers<Dealer>(ctl);


				AddController("dealer", "Автопроизводители", ctl);
			}

			{ //Владельцы
				var ctl = new GenericAPIController<Owner, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["name"] = new ListLambdaBinding<Owner, object>("Имя", x => x.name);

				var fb = ctl.FormBindings;
				fb["name"] = new FormAPIBinding<Owner,object>("Имя", TableForm.ControlTypes.TextBox, x => x.name, (e, v) => e.name=(string)v, () => "");

				ctl.SetupDataGrid();
				ctl.SetupForm();

				ctl.KeySelector = x => x.id;

				AddDefaultCRUDHandlers<Owner>(ctl);


				AddController("owner", "Владельцы", ctl);
			}

			{ //Рода товаров
				var ctl = new GenericAPIController<ProductType, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["name"] = new ListLambdaBinding<ProductType, object>("Название", x => x.name);

				var fb = ctl.FormBindings;
				fb["name"] = new FormAPIBinding<ProductType,object>("Название", TableForm.ControlTypes.TextBox, x => x.name, (e, v) => e.name=(string)v, () => "");

				ctl.SetupDataGrid();
				ctl.SetupForm();

				ctl.KeySelector = x => x.id;

				AddDefaultCRUDHandlers<ProductType>(ctl);


				AddController("product_type", "Рода товаров", ctl);
			}

			{ //Водители
				var ctl = new GenericAPIController<Driver, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["name"] = new ListLambdaBinding<Driver, object>("ФИО", x => x.name);
				cb["exp"] = new ListLambdaBinding<Driver, object>("Начало работы", x => x.experience.ToShortDateString());
				cb["salary"] = new ListLambdaBinding<Driver, object>("Оклад", x => x.salary);

				var fb = ctl.FormBindings;
				fb["name"] = new FormAPIBinding<Driver,object>("ФИО", TableForm.ControlTypes.TextBox, x => x.name, (e, v) => e.name=(string)v, () => "");
				fb["exp"] = new FormAPIBinding<Driver,object>("Начало работы", TableForm.ControlTypes.DateTimePicker, x => x.experience, (e, v) => e.experience=(DateTime)v, () => DateTime.Today);
				fb["salary"] = new FormAPIBinding<Driver,object>("Оклад", TableForm.ControlTypes.NumericUpDown, x => (decimal)x.salary, (e, v) => e.salary=(int)(decimal)v, () => 0m);

				ctl.SetupDataGrid();
				ctl.SetupForm();

				//DateTimePicker
				var dtp = ctl.Form.TableForm.GetControl<DateTimePicker>("exp");
				dtp.Format = DateTimePickerFormat.Short;

				var nud = ctl.Form.TableForm.GetControl<NumericUpDown>("salary");
				nud.Minimum = 0m;
				nud.Maximum = 1000000000m;
				nud.DecimalPlaces = 0;

				ctl.KeySelector = x => x.id;

				AddDefaultCRUDHandlers<Driver>(ctl);


				AddController("driver", "Водители", ctl);
			}
			
			{ //Организации
				var ctl = new GenericAPIController<Organization, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["name"] = new ListLambdaBinding<Organization, object>("Название", x => x.name);
				cb["address"] = new ListLambdaBinding<Organization, object>("Адрес", x => x.address);

				var fb = ctl.FormBindings;
				fb["name"] = new FormAPIBinding<Organization,object>("Название", TableForm.ControlTypes.TextBox, x => x.name, (e, v) => e.name=(string)v, () => "");
				fb["address"] = new FormAPIBinding<Organization,object>("Адрес", TableForm.ControlTypes.TextBox, x => x.address, (e, v) => e.address=(string)v, () => "");

				ctl.SetupDataGrid();
				ctl.SetupForm();
				
				ctl.KeySelector = x => x.id;

				AddDefaultCRUDHandlers<Organization>(ctl);

				AddController("organization", "Организации", ctl);
			}
			
			{ //Склад
				var ctl = new GenericAPIController<Store, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["name"] = new ListLambdaBinding<Store, object>("Название", x => x.name);
				cb["owner"] = new ListLambdaBinding<Store, object>("Владелец", x => x.owner!=null ? x.owner.name : x.owner_id.ToString());

				var fb = ctl.FormBindings;
				fb["name"] = new FormAPIBinding<Store,object>("Название", TableForm.ControlTypes.TextBox, x => x.name, (e, v) => e.name=(string)v, () => "");
				fb["owner"] = new FormAPIBinding<Store,object>("Владелец", TableForm.ControlTypes.ComboBox, x => x.owner.id, (e, v) => e.owner_id=(int)v, () => null);

				ctl.SetupDataGrid();
				ctl.SetupForm();

				var cbox = ctl.Form.TableForm.GetControl<ComboBox>("owner");
				new ComboBoxFilteredAPIDataSource<Owner, int>(cbox, e => e.id, e => e.name, 300.0, false, false, s => GetAll<Owner>().Where(e => e.name.ToLower().StartsWith(s)), false);
				
				ctl.KeySelector = x => x.id;

				AddDefaultCRUDHandlers<Store>(ctl);

				AddController("store", "Склад", ctl);
			}
			
			{ //Товары
				var ctl = new GenericAPIController<misha_api.types.Product, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["name"] = new ListLambdaBinding<misha_api.types.Product, object>("Название", x => x.name);
				cb["type"] = new ListLambdaBinding<misha_api.types.Product, object>("Род", x => x.type!=null ? x.type.name : x.type_id.ToString());
				cb["weight"] = new ListLambdaBinding<misha_api.types.Product, object>("Вес", x => x.weight);

				var fb = ctl.FormBindings;
				fb["name"] = new FormAPIBinding<misha_api.types.Product,object>("Название", TableForm.ControlTypes.TextBox, x => x.name, (e, v) => e.name=(string)v, () => "");
				fb["type"] = new FormAPIBinding<misha_api.types.Product,object>("Род", TableForm.ControlTypes.ComboBox, x => x.type.id, (e, v) => e.type_id=(int)v, () => null);
				fb["weight"] = new FormAPIBinding<misha_api.types.Product,object>("Вес", TableForm.ControlTypes.NumericUpDown, x => (decimal)x.weight, (e, v) => e.weight=(int)(decimal)v, () => 0m);

				ctl.SetupDataGrid();
				ctl.SetupForm();

				var nud = ctl.Form.TableForm.GetControl<NumericUpDown>("weight");
				nud.Minimum = 0m;
				nud.Maximum = 1000000000m;
				nud.DecimalPlaces = 0;

				var cbox = ctl.Form.TableForm.GetControl<ComboBox>("type");
				new ComboBoxFilteredAPIDataSource<ProductType, int>(cbox, e => e.id, e => e.name, 300.0, false, false, s => GetAll<ProductType>().Where(e => e.name.ToLower().StartsWith(s)), false);
				
				ctl.KeySelector = x => x.id;

				AddDefaultCRUDHandlers<misha_api.types.Product>(ctl);

				AddController("product", "Товары", ctl);
			}
			
			{ //Груз
				var ctl = new GenericAPIController<Shipment, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["transport"] = new ListLambdaBinding<Shipment, object>("Перевозка", x => x.transportation!=null ? x.transportation.id : x.transportation_id);
				cb["product"] = new ListLambdaBinding<Shipment, object>("Товар", x => x.product!=null ? x.product.name : x.product_id.ToString());
				cb["amount"] = new ListLambdaBinding<Shipment, object>("Количество", x => x.amount);

				var fb = ctl.FormBindings;
				fb["transport"] = new FormAPIBinding<Shipment,object>("Перевозка", TableForm.ControlTypes.ComboBox, x => x.transportation.id, (e, v) => e.transportation_id=(int)v, () => null);
				fb["product"] = new FormAPIBinding<Shipment,object>("Товар", TableForm.ControlTypes.ComboBox, x => x.product.id, (e, v) => e.product_id=(int)v, () => null);
				fb["amount"] = new FormAPIBinding<Shipment,object>("Количество", TableForm.ControlTypes.NumericUpDown, x => (decimal)x.amount, (e, v) => e.amount=(int)(decimal)v, () => 0m);

				ctl.SetupDataGrid();
				ctl.SetupForm();

				var nud = ctl.Form.TableForm.GetControl<NumericUpDown>("amount");
				nud.Minimum = 0m;
				nud.Maximum = 1000000000m;
				nud.DecimalPlaces = 0;

				var cbox = ctl.Form.TableForm.GetControl<ComboBox>("transport");
				new ComboBoxFilteredAPIDataSource<Transportation, int>(cbox, e => e.id, e => e.id.ToString(), 300.0, false, false, s => GetAll<Transportation>().Where(e => e.id.ToString().StartsWith(s)), false);

				cbox = ctl.Form.TableForm.GetControl<ComboBox>("product");
				new ComboBoxFilteredAPIDataSource<misha_api.types.Product, int>(cbox, e => e.id, e => e.name, 300.0, false, false, s => GetAll<misha_api.types.Product>().Where(e => e.name.ToLower().StartsWith(s)), false);
				
				ctl.KeySelector = x => x.id;

				AddDefaultCRUDHandlers<Shipment>(ctl);

				AddController("shipment", "Грузы", ctl);
			}
			
			{ //Перевозка
				var ctl = new GenericAPIController<Transportation, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["id"] = new ListLambdaBinding<Transportation, object>("Номер", x => x.id);
				cb["date"] = new ListLambdaBinding<Transportation, object>("Дата", x => x.date);
				cb["car"] = new ListLambdaBinding<Transportation, object>("Машина", x => x.car!=null ? x.car.id : x.car_id);
				cb["org"] = new ListLambdaBinding<Transportation, object>("Организация", x => x.organization!=null ? x.organization.name : x.organization_id.ToString());
				cb["store"] = new ListLambdaBinding<Transportation, object>("Склад", x => x.store!=null ? x.store.name : x.store_id.ToString());

				var fb = ctl.FormBindings;
				fb["date"] = new FormAPIBinding<Transportation,object>("Дата перевозки", TableForm.ControlTypes.DateTimePicker, x => x.date, (e, v) => e.date=(DateTime)v, () => DateTime.Today);
				fb["car"] = new FormAPIBinding<Transportation,object>("Машина", TableForm.ControlTypes.ComboBox, x => x.car.id, (e, v) => e.car_id=(int)v, () => null);
				fb["org"] = new FormAPIBinding<Transportation,object>("Организация", TableForm.ControlTypes.ComboBox, x => x.organization.id, (e, v) => e.organization_id=(int)v, () => null);
				fb["store"] = new FormAPIBinding<Transportation,object>("Склад", TableForm.ControlTypes.ComboBox, x => x.store.id, (e, v) => e.store_id=(int)v, () => null);

				ctl.SetupDataGrid();
				ctl.SetupForm();

				ctl.KeySelector = x => x.id;

				var cbox = ctl.Form.TableForm.GetControl<ComboBox>("car");
				new ComboBoxFilteredAPIDataSource<Car, int>(cbox, e => e.id, e => e.id.ToString(), 300.0, false, false, s => GetAll<Car>().Where(e => e.id.ToString().StartsWith(s)), false);

				cbox = ctl.Form.TableForm.GetControl<ComboBox>("org");
				new ComboBoxFilteredAPIDataSource<Organization, int>(cbox, e => e.id, e => e.name, 300.0, false, false, s => GetAll<Organization>().Where(e => e.name.ToLower().StartsWith(s)), false);
				
				cbox = ctl.Form.TableForm.GetControl<ComboBox>("store");
				new ComboBoxFilteredAPIDataSource<Store, int>(cbox, e => e.id, e => e.name, 300.0, false, false, s => GetAll<Store>().Where(e => e.name.ToLower().StartsWith(s)), false);
				

				AddDefaultCRUDHandlers<Transportation>(ctl);

				AddController("transport", "Перевозки", ctl);
			}
			
			{ //Машина
				var ctl = new GenericAPIController<Car, int>(splitCont.Panel2);

				var cb = ctl.ColumnBindings;
				cb["id"] = new ListLambdaBinding<Car, object>("Номер", x => x.id);
				cb["model"] = new ListLambdaBinding<Car, object>("Модель", x => x.model);
				cb["capacity"] = new ListLambdaBinding<Car, object>("Грузоподъёмность", x => x.capacity);
				cb["dealer"] = new ListLambdaBinding<Car, object>("Дилер", x => x.dealer!=null ? x.dealer.name : x.dealer_id.ToString());
				cb["driver"] = new ListLambdaBinding<Car, object>("Водитель", x => x.driver!=null ? x.driver.name : x.driver_id.ToString());
				cb["owner"] = new ListLambdaBinding<Car, object>("Владелец", x => x.owner!=null ? x.owner.name : x.owner_id.ToString());

				var fb = ctl.FormBindings;
				fb["model"] = new FormAPIBinding<Car,object>("Модель", TableForm.ControlTypes.TextBox, x => x.model, (e, v) => e.model=(string)v, () => "");
				fb["capacity"] = new FormAPIBinding<Car,object>("Грузоподъёмность", TableForm.ControlTypes.NumericUpDown, x => (decimal)x.capacity, (e, v) => e.capacity=(int)(decimal)v, () => 0m);
				fb["dealer"] = new FormAPIBinding<Car,object>("Дилер", TableForm.ControlTypes.ComboBox, x => x.dealer.id, (e, v) => e.dealer_id=(int)v, () => null);
				fb["driver"] = new FormAPIBinding<Car,object>("Водитель", TableForm.ControlTypes.ComboBox, x => x.driver.id, (e, v) => e.driver_id=(int)v, () => null);
				fb["owner"] = new FormAPIBinding<Car,object>("Владелец", TableForm.ControlTypes.ComboBox, x => x.owner.id, (e, v) => e.owner_id=(int)v, () => null);

				ctl.SetupDataGrid();
				ctl.SetupForm();
				
				var nud = ctl.Form.TableForm.GetControl<NumericUpDown>("capacity");
				nud.Minimum = 0m;
				nud.Maximum = 1000000000m;
				nud.DecimalPlaces = 0;

				var cbox = ctl.Form.TableForm.GetControl<ComboBox>("dealer");
				new ComboBoxFilteredAPIDataSource<Dealer, int>(cbox, e => e.id, e => e.name, 300.0, false, false, s => GetAll<Dealer>().Where(e => e.name.ToLower().StartsWith(s)), false);

				cbox = ctl.Form.TableForm.GetControl<ComboBox>("driver");
				new ComboBoxFilteredAPIDataSource<Driver, int>(cbox, e => e.id, e => e.name, 300.0, false, false, s => GetAll<Driver>().Where(e => e.name.ToLower().StartsWith(s)), false);
				
				cbox = ctl.Form.TableForm.GetControl<ComboBox>("owner");
				new ComboBoxFilteredAPIDataSource<Owner, int>(cbox, e => e.id, e => e.name, 300.0, false, false, s => GetAll<Owner>().Where(e => e.name.ToLower().StartsWith(s)), false);
				
				ctl.KeySelector = x => x.id;

				AddDefaultCRUDHandlers<Car>(ctl);

				AddController("car", "Машины", ctl);
			}

			treeView.ResumeLayout();
		}

		void AddDefaultCRUDHandlers<TEntity>(GenericAPIController<TEntity, int> ctl) 
			where TEntity:class, new()
		{

			ctl.GetListCount = () => {
				using (var c= new MishaClient()) {
					return c.GetCount<TEntity>();
				}
			};

			ctl.GetList = (skip, take) => {
				using (var c= new MishaClient()) {
					return c.GetMany<TEntity>(skip, take) ?? new TEntity[0];
				}
			};

			ctl.GetEntity = (id) => {
				using (var c= new MishaClient()) {
					return c.Get<TEntity>(id);
				}
			};

			ctl.CreateEntity = () => {
				return new TEntity();
			};

			ctl.InsertEntity = (e) => {
				using (var c= new MishaClient()) {
					c.Create<TEntity>(e);
				}
			};

			ctl.UpdateEntity = (e) => {
				using (var c= new MishaClient()) {
					c.Update<TEntity>(ctl.KeySelector(e), e);
				}
			};

			ctl.DeleteByKey = (id) => {
				using (var c= new MishaClient()) {
					c.Delete<TEntity>(id);
				}
			};

			AddCBRefreshHandlers(ctl);
		}
		
		void AddCBRefreshHandlers<TEntity>(GenericAPIController<TEntity, int> ctl) 
			where TEntity:class, new()
		{
			ctl.CustomDefault = (f) => {
				foreach (var b in ctl.FormBindings) {
					if (b.Value.ControlType == TableForm.ControlTypes.ComboBox) {
						var cb = f.GetControl<ComboBox>(b.Key);

						if (cb!=null) {
							var src = cb.DataSource as common.IRefreshable;

							if (src!=null) src.Refresh();
						}
					}
				}
			};

			ctl.CustomLoad = (f, e) => {
				foreach (var b in ctl.FormBindings) {
					if (b.Value.ControlType == TableForm.ControlTypes.ComboBox) {
						var cb = f.GetControl<ComboBox>(b.Key);

						if (cb!=null) {
							var src = cb.DataSource as common.IRefreshable;

							if (src!=null) src.Refresh();
						}
					}
				}
			};
		}
		
		void AddCBRefreshHandlers<TEntity>(tp_lab.mongo.GenericMongoController<TEntity, int> ctl) 
			where TEntity:class, new()
		{
			ctl.CustomDefault = (f, c) => {
				foreach (var b in ctl.FormBindings) {
					if (b.Value.ControlType == TableForm.ControlTypes.ComboBox) {
						var cb = f.GetControl<ComboBox>(b.Key);

						if (cb!=null) {
							var src = cb.DataSource as common.IRefreshable;

							if (src!=null) src.Refresh();
						}
					}
				}
			};

			ctl.CustomLoad = (f, c, e) => {
				foreach (var b in ctl.FormBindings) {
					if (b.Value.ControlType == TableForm.ControlTypes.ComboBox) {
						var cb = f.GetControl<ComboBox>(b.Key);

						if (cb!=null) {
							var src = cb.DataSource as common.IRefreshable;

							if (src!=null) src.Refresh();
						}
					}
				}
			};
		}

		List<T> GetAll<T>() where T : class
		{
			using (var c = new MishaClient()) {
				/*int count = c.GetCount<T>();
				List<T> ret = new List<T>(count);
				int i=0;

				while (i<count) {
					ret.AddRange(c.GetMany<T>(i, 10));
					i += 10;
				}

				return ret;*/
				return c.GetMany<T>(0, int.MaxValue).ToList();
			}

			return new List<T>();
		}
	}
}
