using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_lab.api {
	partial class TPService : ITPService {		
		public bool SetAttribute(types.Attribute value)
		{
			using (Entities ctx=new Entities()) {
				try {
					ctx.attribute.Find(value.id).name = value.Name;
					ctx.SaveChanges();
					return true;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetCategory(types.Category value)
		{
			using (Entities ctx=new Entities()) {
				try {
					Category e = ctx.category.Find(value.id);
					e.name = value.Name;
					e.parent_id = value.ParentID;

					ctx.SaveChanges();
					return true;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetCustomer(types.Customer value)
		{
			using (Entities ctx=new Entities()) {
				try {
					Customer e = ctx.customer.Find(value.id);
					e.first_name = value.FirstName;
					e.middle_name = value.MiddleName;
					e.last_name = value.LastName;
					e.email = value.Email;
					e.phone = value.Phone;

					ctx.SaveChanges();
					return true;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetCustomerAddress(types.CustomerAddress value)
		{
			using (Entities ctx=new Entities()) {
				try {
					Address e = ctx.address.Find(value.id);
					e.country = value.Country;
					e.city = value.City;
					e.address = value.Address;
					e.postcode = value.Postcode;

					ctx.SaveChanges();
					return true;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetManufacturer(types.Manufacturer value)
		{
			using (Entities ctx=new Entities()) {
				try {
					Manufacturer e = ctx.manufacturer.Find(value.id);
					e.name = value.Name;

					ctx.SaveChanges();
					return true;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetOrderStatus(types.OrderStatus value)
		{
			using (Entities ctx=new Entities()) {
				try {
					OrderStatus e = ctx.order_status.Find(value.id);
					e.name = value.Name;

					ctx.SaveChanges();
					return true;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetProduct(types.Product value)
		{
			using (Entities ctx=new Entities()) {
				try {
					Product e = ctx.product.Find(value.id);
					e.name = value.Name;
					e.model = value.Model;
					e.description = value.Description;
					e.category_id = value.CategoryID;
					e.manufacturer_id = value.ManufacturerID;
					e.price = value.Price;
					e.status_id = value.StatusID;

					if (value.Attributes!=null) {
						foreach (ProductAttribute atr in e.attributes) {
							types.ProductAttribute a = value.Attributes.Find(x => x.AttributeID==atr.attribute_id);

							// Изменяем или удаляем существующие атрибуты
							if (a!=null)
								atr.value = a.Value;
							else
								ctx.product_attribute.Remove(atr);
						}

						// Добавляем новые
						foreach (types.ProductAttribute a in value.Attributes) {
							if (!e.attributes.All(x => x.attribute_id!=a.AttributeID)) {
								ProductAttribute pa = ctx.product_attribute.Create();
								pa.attribute_id = a.AttributeID;
								pa.value = a.Value;
								e.attributes.Add(pa);
							}
						}
					}

					ctx.SaveChanges();
					return true;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetProductAttribute(types.ProductAttribute value)
		{
			using (Entities ctx=new Entities()) {
				try {
					ProductAttribute e = ctx.product_attribute.Find(value.id);
					e.attribute_id = value.AttributeID;
					e.value = value.Value;

					ctx.SaveChanges();
					return true;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetProductStatus(types.ProductStatus value)
		{
			using (Entities ctx=new Entities()) {
				try {
					ProductStatus e = ctx.product_status.Find(value.id);
					e.name = value.Name;

					ctx.SaveChanges();
					return true;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}
	}
}
