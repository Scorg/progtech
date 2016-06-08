using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_lab.api {
	partial class TPService: ITPService {
		public int AddAttribute(types.Attribute v)
		{
			using (Entities ctx=new Entities()) {
				try {
					Attribute e = ctx.attribute.Create();
					e.name = v.Name;

					ctx.attribute.Add(e);
					ctx.SaveChanges();
					return e.id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddCategory(types.Category v)
		{
			using (Entities ctx=new Entities()) {
				try {
					Category e = ctx.category.Create();
					e.name = v.Name;
					e.parent_id = v.ParentID;

					ctx.category.Add(e);
					ctx.SaveChanges();
					return e.id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddCustomer(types.Customer v)
		{
			using (Entities ctx=new Entities()) {
				try {
					Customer e = ctx.customer.Create();
					e.enabled = true;
					e.first_name = v.FirstName;
					e.middle_name = v.MiddleName;
					e.last_name = v.LastName;
					e.phone = v.Phone;
					e.email = v.Email;

					if (v.Addresses!=null)
					v.Addresses.ForEach(x => e.addresses.Add(new Address {
																country=x.Country,
																city=x.City,
																address=x.Address,
																postcode=x.Postcode
					}));

					ctx.customer.Add(e);
					ctx.SaveChanges();
					return e.id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddCustomerAddress(types.CustomerAddress v)
		{
			using (Entities ctx=new Entities()) {
				try {
					Address e = ctx.address.Create();
					e.customer_id = v.CustomerID;
					e.country = v.Country;
					e.city = v.City;
					e.address = v.Address;
					e.postcode = v.Postcode;

					ctx.address.Add(e);
					ctx.SaveChanges();
					return e.id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddManufacturer(types.Manufacturer v)
		{
			using (Entities ctx=new Entities()) {
				try {
					Manufacturer e = ctx.manufacturer.Create();
					e.name = v.Name;

					ctx.manufacturer.Add(e);
					ctx.SaveChanges();
					return e.id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddOrder(types.OrderRequest v)
		{
			using (Entities ctx=new Entities()) {
				try {
					Order e = ctx.order.Create();
					e.status_id = ctx.order_status.First().id;
					e.date = DateTime.Now;

					Customer c = ctx.customer.Find(v.CustomerID);
					e.customer_id = v.CustomerID;
					e.first_name = c.first_name;
					e.middle_name = c.middle_name;
					e.last_name = c.last_name;

					Address a = c.addresses.Single(x => x.id==v.AddressID);
					e.country = a.country;
					e.city = a.city;
					e.address = a.address;
					e.postcode = a.postcode;

					v.Products.ForEach(x => {
						OrderProduct op = ctx.order_product.Create();
						Product p = ctx.product.First(y => y.id==x.ProductID);
						op.product_id = x.ProductID;
						op.price = p.price;
						op.quantity = x.Quantity;
						e.products.Add(op);
					});

					ctx.order.Add(e);
					ctx.SaveChanges();
					return e.id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddOrderStatus(types.OrderStatus v)
		{
			using (Entities ctx=new Entities()) {
				try {
					OrderStatus e = ctx.order_status.Create();
					e.name = v.Name;

					ctx.order_status.Add(e);
					ctx.SaveChanges();
					return e.id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddProduct(types.Product v)
		{
			using (Entities ctx=new Entities()) {
				try {
					Product e = ctx.product.Create();
					e.name = v.Name;
					e.model = v.Model;
					e.description = v.Description;
					e.category_id = v.CategoryID;
					e.manufacturer_id = v.ManufacturerID;
					e.price = v.Price;
					e.status_id = v.StatusID;

					if (v.Attributes!=null)
						v.Attributes.ForEach(x => {
							ProductAttribute pa = ctx.product_attribute.Create();
							pa.attribute_id = x.AttributeID;
							pa.value = x.Value;
							e.attributes.Add(pa);
						});

					ctx.product.Add(e);
					ctx.SaveChanges();
					return e.id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddProductAttribute(types.ProductAttribute v)
		{
			using (Entities ctx=new Entities()) {
				try {
					ProductAttribute e = ctx.product_attribute.Create();
					e.attribute_id = v.AttributeID;
					e.product_id = v.ProductID;
					e.value = v.Value;

					ctx.product_attribute.Add(e);
					ctx.SaveChanges();
					return e.id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddProductStatus(types.ProductStatus v)
		{
			using (Entities ctx=new Entities()) {
				try {
					ProductStatus e = ctx.product_status.Create();
					e.name = v.Name;

					ctx.product_status.Add(e);
					ctx.SaveChanges();
					return e.id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}
	}
}
