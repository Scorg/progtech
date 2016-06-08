using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace tp_lab.api {
	partial class TPService: ITPService {
		public int AddAttribute(types.Attribute v)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					int id = MongoCounter<Attribute>.NextValue();

					ctx.Set<Attribute>().InsertOne(
						new Attribute {
							id = id,
							name = v.Name
						});

					return id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddCategory(types.Category v)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					int id = MongoCounter<Category>.NextValue();

					ctx.Set<Category>().InsertOne(
						new Category {
							id = id,
							name = v.Name,
							parent_id = v.ParentID
						});

					return id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddCustomer(types.Customer v)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {					
					int id = MongoCounter<Customer>.NextValue();

					ctx.Set<Customer>().InsertOne(
						new Customer {
							id = id,
							enabled = true,
							first_name = v.FirstName,
							middle_name = v.MiddleName,
							last_name = v.LastName,
							phone = v.Phone,
							email = v.Email,

							addresses = v.Addresses!=null 
								? v.Addresses.Select(
									x => new Address {
										id = MongoCounter<Address>.NextValue(),
										customer_id = id,
										country=x.Country,
										city=x.City,
										address=x.Address,
										postcode=x.Postcode
									}).ToList()
								: null
						});

					return id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddCustomerAddress(types.CustomerAddress v)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					int id = MongoCounter<Address>.NextValue();

					var filter = Builders<Customer>.Filter.Eq(x => x.id, v.CustomerID);
					var update = Builders<Customer>.Update
						.AddToSet(x => x.addresses,
							new Address {
								id = id,
								customer_id = v.CustomerID,
								country = v.Country,
								city = v.City,
								address = v.Address,
								postcode = v.Postcode
							});

					var result = ctx.Set<Customer>().UpdateOne(filter, update);

					if (result.IsAcknowledged && result.ModifiedCount==1) return id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddManufacturer(types.Manufacturer v)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					int id = MongoCounter<Manufacturer>.NextValue();

					ctx.Set<Manufacturer>().InsertOne(
						new Manufacturer {
							id = id,
							name = v.Name
						});

					return id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddOrder(types.OrderRequest v)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					Order e = new Order();
					e.status_id = ctx.Set<OrderStatus>().AsQueryable().First().id;
					e.date = DateTime.Now;
					
					Customer c = ctx.Set<Customer>().Find(x => x.id == v.CustomerID).First();

					if (c==null) {
						common.LogFile.Log("Невозможно добавить заказ: пользователь с id = " + v.CustomerID.ToString() + " не найден в базе");
						return -1;
					}

					e.customer_id = v.CustomerID;
					e.first_name = c.first_name;
					e.middle_name = c.middle_name;
					e.last_name = c.last_name;

					Address a = c.addresses.First(x => x.id==v.AddressID);

					if (a==null) {
						common.LogFile.Log("Невозможно добавить заказ: адрес пользователя с id = " + v.AddressID.ToString() + " не найден в базе");
						return -1;
					}

					e.country = a.country;
					e.city = a.city;
					e.address = a.address;
					e.postcode = a.postcode;
					
					if (v.Products==null || v.Products.Count==0) {
						common.LogFile.Log("Невозможно добавить заказ: список товаров пуст");
						return -1;
					}

					e.products = v.Products.Select(x => new OrderProduct {
						product_id = x.ProductID,
						price = ctx.Set<Product>().Find(y => y.id==x.ProductID).First().price,
						quantity = x.Quantity
					}).ToList();
					
					e.id = MongoCounter<Order>.NextValue();
					ctx.Set<Order>().InsertOne(e);

					return e.id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddOrderStatus(types.OrderStatus v)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					int id = MongoCounter<OrderStatus>.NextValue();

					ctx.Set<OrderStatus>().InsertOne(
						new OrderStatus {
							id = id,
							name = v.Name
						});

					return id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddProduct(types.Product v)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					Product e = new Product();
					e.name = v.Name;
					e.model = v.Model;
					e.description = v.Description;
					e.price = v.Price;

					e.category_id = v.CategoryID;
					e.manufacturer_id = v.ManufacturerID;
					e.status_id = v.StatusID;

					if (v.Attributes!=null) {
						e.attributes = v.Attributes.Select(x => new ProductAttribute {
							id = MongoCounter<ProductAttribute>.NextValue(),
							product_id = v.id,
							attribute_id = x.AttributeID,
							value = x.Value
						}).ToList();
					}

					e.id = MongoCounter<Product>.NextValue();
					ctx.Set<Product>().InsertOne(e);

					return e.id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddProductAttribute(types.ProductAttribute v)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					int id = MongoCounter<ProductAttribute>.NextValue();

					var filter = Builders<Product>.Filter.Eq(x => x.id, v.ProductID);
					var update = Builders<Product>.Update
						.AddToSet(x => x.attributes,
							new ProductAttribute {
								id = id,
								product_id = v.ProductID,
								attribute_id = v.AttributeID,
								value = v.Value
							});

					var result = ctx.Set<Product>().UpdateOne(filter, update);

					if (result.IsAcknowledged && result.ModifiedCount==1) return id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}

		public int AddProductStatus(types.ProductStatus v)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					int id = MongoCounter<ProductStatus>.NextValue();

					ctx.Set<ProductStatus>().InsertOne(
						new ProductStatus {
							id = id,
							name = v.Name
						});

					return id;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return -1;
			}
		}
	}
}
