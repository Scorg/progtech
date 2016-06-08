using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
//using common;
//using tp_lab;

namespace tp_lab.api {
	partial class TPService : ITPService {
		public types.Category GetCategory(int id)
		{
			using (Entities ctx=new Entities()) {
				try {
					Category c = ctx.category.Find(id);
					if (c==null) return null;

					return new types.Category {
						id=c.id,
						Name = c.name,
						ParentID = c.parent_id
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.Manufacturer GetManufacturer(int id)
		{
			using (Entities ctx=new Entities()) {
				try {
					Manufacturer e = ctx.manufacturer.Find(id);
					if (e==null) return null;

					return new types.Manufacturer {
						id = e.id,
						Name = e.name
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.Product GetProduct(int id)
		{
			using (Entities ctx=new Entities()) {
				try {
					Product e = ctx.product.Find(id);
					if (e==null) return null;

					return new types.Product {
						id = e.id,
						Name = e.name,
						Model = e.model,
						Description = e.description,
						Price = e.price,
						ManufacturerID = e.manufacturer_id,
						CategoryID = e.category_id,
						StatusID = e.status_id,
						Attributes = e.attributes.Select(x => new types.ProductAttribute {
							id = x.id,
							AttributeID = x.attribute_id,
							Value = x.value
						}).ToList()
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.Attribute GetAttribute(int id)
		{
			using (Entities ctx=new Entities()) {
				try {
					Attribute e = ctx.attribute.Find(id);
					if (e==null) return null;

					return new types.Attribute {
						id = e.id,
						Name = e.name
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.ProductStatus GetProductStatus(int id)
		{
			using (Entities ctx=new Entities()) {
				try {
					ProductStatus e = ctx.product_status.Find(id);
					if (e==null) return null;

					return new types.ProductStatus {
						id = e.id,
						Name = e.name
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.Order GetOrder(int id)
		{
			using (Entities ctx=new Entities()) {
				try {
					Order e = ctx.order.Find(id);
					if (e==null) return null;

					return new types.Order {
						id = e.id,
						CustomerID = e.customer_id,
						StatusID = e.status_id,
						Date = e.date,
						FirstName = e.first_name,
						MiddleName = e.middle_name,
						LastName = e.last_name,
						Country = e.country,
						City = e.city,
						Address = e.address,
						Postcode = e.postcode,
						Products = e.products.Select(x => new types.OrderProduct {
							id = x.id,
							ProductID = x.product_id,
							Quantity = x.quantity,
							Price = x.price
						}).ToList()
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.OrderStatus GetOrderStatus(int id)
		{
			using (Entities ctx=new Entities()) {
				try {
					OrderStatus e = ctx.order_status.Find(id);
					if (e==null) return null;

					return new types.OrderStatus {
						id = e.id,
						Name = e.name
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.Customer GetCustomer(int id)
		{
			using (Entities ctx=new Entities()) {
				try {
					Customer e = ctx.customer.Find(id);
					if (e==null) return null;

					if (e.enabled!=true) return null;

					return new types.Customer {
						id = e.id,
						FirstName = e.first_name,
						MiddleName = e.middle_name,
						LastName = e.last_name,
						Phone = e.phone,
						Email = e.email,
						Addresses = e.addresses.Select(a => new types.CustomerAddress {
							id = a.id,
							Country = a.country,
							City = a.city,
							Address = a.address,
							Postcode = a.postcode
						}).ToList()
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public types.CustomerAddress GetCustomerAddress(int id)
		{
			using (Entities ctx=new Entities()) {
				try {
					Address e = ctx.address.Find(id);

					if (e==null) return null;

					return new types.CustomerAddress {
						id = e.id,
						CustomerID = e.customer_id,
						Country = e.country,
						City = e.city,
						Address = e.address,
						Postcode = e.postcode
					};
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}



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

					Customer c = ctx.customer.Find(new object[]{v.CustomerID});
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



		
		public List<types.Attribute> GetAttributes()
		{
			using (Entities ctx=new Entities()) {
				try {
					return ctx.attribute.Select(x => new types.Attribute { id=x.id, Name=x.name }).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public List<types.Category> GetCategories()
		{
			using (Entities ctx=new Entities()) {
				try {
					return ctx.category.Select(x => new types.Category { id=x.id, ParentID=x.parent_id, Name=x.name }).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public List<types.Customer> GetCustomers(int skip, int count)
		{
			using (Entities ctx=new Entities()) {
				try {
					return ctx.customer.OrderBy(x => x.login).Skip(skip).Take(count)
						.Select(x => new types.Customer {
							id = x.id,
							Email = x.email,
							FirstName = x.first_name,
							LastName = x.first_name,
							MiddleName = x.middle_name,
							Phone = x.phone
						}).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public List<types.CustomerAddress> GetCustomerAddresses(int id)
		{
			using (Entities ctx=new Entities()) {
				try {					
					return ctx.customer.Find(id).addresses
						.Select(x => new types.CustomerAddress {
							id=x.id,
							Country=x.country,
							City=x.city,
							Address=x.address,
							Postcode=x.postcode
						}).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public List<types.Manufacturer> GetManufacturers()
		{
			using (Entities ctx=new Entities()) {
				try {					
					return ctx.manufacturer.Select(x => new types.Manufacturer {
						id=x.id,
						Name = x.name
					}).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public List<types.Order> GetOrders(int skip, int count)
		{
			using (Entities ctx=new Entities()) {
				try {					
					return ctx.order.OrderByDescending(x => x.date).Skip(skip).Take(count)
						.Select(x => new types.Order {
							id=x.id,
							Date=x.date,
							StatusID=x.status_id,

							CustomerID=x.customer_id,
							FirstName=x.first_name,
							MiddleName=x.middle_name,

							LastName=x.last_name,
							Country=x.country,
							City=x.city,
							Address=x.address,
							Postcode=x.postcode
						}).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public List<types.OrderProduct> GetOrderProducts(int order)
		{
			using (Entities ctx=new Entities()) {
				try {					
					return ctx.order.Find(order).products
						.Select(x => new types.OrderProduct {
						id=x.id,
						ProductID=x.product_id,
						Price=x.price,
						Quantity=x.quantity
					}).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}

		public List<types.Product> GetProducts(int skip, int count)
		{
			using (Entities ctx=new Entities()) {
				try {					
					return ctx.product.OrderBy(x => x.id).Skip(skip).Take(count)
						.Select(x => new types.Product {
							id=x.id,
							Name=x.name,
							Model=x.model,
							Description=x.description,
							Price=x.price,

							ManufacturerID=x.manufacturer_id,
							StatusID=x.status_id,
							CategoryID=x.category_id,

							Attributes = x.attributes.Select(y => new types.ProductAttribute {
								id=y.id,
								AttributeID=y.attribute_id,
								Value=y.value
							}).ToList()
						}).ToList();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return null;
			}
		}




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
