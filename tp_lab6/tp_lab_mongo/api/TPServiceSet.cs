using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace tp_lab.api {
	partial class TPService : ITPService {		
		public bool SetAttribute(types.Attribute value)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					var rt = ctx.Set<Attribute>().UpdateOne(x => x.id == value.id, 
						Builders<Attribute>.Update
							.Set(x => x.name, value.Name)
						);

					return rt.IsAcknowledged && rt.ModifiedCount==1;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetCategory(types.Category value)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					var rt = ctx.Set<Category>().UpdateOne(x => x.id == value.id, 
						Builders<Category>.Update
							.Set(x => x.name, value.Name)
							.Set(x => x.parent_id, value.ParentID)
						);

					return rt.IsAcknowledged && rt.ModifiedCount==1;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetCustomer(types.Customer value)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					var updatedef = Builders<Customer>.Update
							.Set(x => x.first_name, value.FirstName)
							.Set(x => x.middle_name, value.MiddleName)
							.Set(x => x.last_name, value.LastName)
							.Set(x => x.email, value.Email)
							.Set(x => x.phone, value.Phone);

					if (value.Addresses!=null) {
						updatedef.Set(x => x.addresses,
							value.Addresses.Select(
							x => new tp_lab.Address {
								id = MongoCounter<Address>.NextValue(),
								customer_id = value.id,
								country = x.Country,
								city = x.City,
								address = x.Address,
								postcode = x.Postcode
							}));
					}

					var rt = ctx.Set<Customer>().UpdateOne(x => x.id == value.id, updatedef);

					return rt.IsAcknowledged && rt.ModifiedCount==1;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetCustomerAddress(types.CustomerAddress value)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					var filter = Builders<Customer>.Filter.Where(x => x.id == value.CustomerID && x.addresses.Any(y => y.id == value.id));

					var updatedef = Builders<Customer>.Update
						//.Set(x => x.addresses.AsEnumerable().ElementAt(-1).id, value.id)
						//.Set(x => x.addresses.AsEnumerable().ElementAt(-1).customer_id, value.CustomerID)
						.Set(x => x.addresses.AsEnumerable().ElementAt(-1).country, value.Country)
						.Set(x => x.addresses.AsEnumerable().ElementAt(-1).city, value.City)
						.Set(x => x.addresses.AsEnumerable().ElementAt(-1).address, value.Address)
						.Set(x => x.addresses.AsEnumerable().ElementAt(-1).postcode, value.Postcode);

					var rt = ctx.Set<Customer>().UpdateOne(filter, updatedef);

					return rt.IsAcknowledged && rt.ModifiedCount==1;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetManufacturer(types.Manufacturer value)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					var rt = ctx.Set<Manufacturer>().UpdateOne(x => x.id == value.id, 
						Builders<Manufacturer>.Update
							.Set(x => x.name, value.Name)
						);

					return rt.IsAcknowledged && rt.ModifiedCount==1;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetOrderStatus(types.OrderStatus value)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					var rt = ctx.Set<OrderStatus>().UpdateOne(x => x.id == value.id, 
						Builders<OrderStatus>.Update
							.Set(x => x.name, value.Name)
						);

					return rt.IsAcknowledged && rt.ModifiedCount==1;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetProduct(types.Product value)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					var updatedef = Builders<Product>.Update
						.Set(e => e.name, value.Name)
						.Set(e => e.model, value.Model)
						.Set(e => e.description, value.Description)
						.Set(e => e.category_id, value.CategoryID)
						.Set(e => e.manufacturer_id, value.ManufacturerID)
						.Set(e => e.price, value.Price)
						.Set(e => e.status_id, value.StatusID);

					if (value.Attributes!=null) {
						updatedef.Set(x => x.attributes,
							value.Attributes.Select(
								x => new ProductAttribute {
									id = MongoCounter<Attribute>.NextValue(),
									product_id = value.id,
									attribute_id = x.AttributeID,
									value = x.Value
								}));
					}
					
					var rt = ctx.Set<Product>().UpdateOne(x => x.id == value.id, updatedef);

					return rt.IsAcknowledged && rt.ModifiedCount==1;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetProductAttribute(types.ProductAttribute value)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					var filter = Builders<Product>.Filter.Where(x => x.id == value.ProductID && x.attributes.Any(y => y.id == value.id));

					var updatedef = Builders<Product>.Update.AddToSet(x => x.attributes,
							 new tp_lab.ProductAttribute {
								//id = value.id,
								//product_id = value.ProductID,
								attribute_id = value.AttributeID,
								value = value.Value
							});

					var rt = ctx.Set<Product>().UpdateOne(filter, updatedef);

					return rt.IsAcknowledged && rt.ModifiedCount==1;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}

		public bool SetProductStatus(types.ProductStatus value)
		{
			using (MongoContext ctx=new MongoContext()) {
				try {
					var rt = ctx.Set<ProductStatus>().UpdateOne(x => x.id == value.id, 
						Builders<ProductStatus>.Update
							.Set(x => x.name, value.Name)
						);

					return rt.IsAcknowledged && rt.ModifiedCount==1;
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return false;
			}
		}
	}
}
