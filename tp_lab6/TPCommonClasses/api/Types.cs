using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace tp_lab.api.types {
	[DataContract]
	public class Category {
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public int? ParentID { get; set; }
	}

	[DataContract]
	public class Product {
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string Model { get; set; }
		[DataMember]
		public string Description { get; set; }
		[DataMember]
		public decimal Price { get; set; }
		[DataMember]
		public int ManufacturerID { get; set; }
		[DataMember]
		public int? CategoryID { get; set; }
		[DataMember]
		public int StatusID { get; set; }
		[DataMember]
		public List<ProductAttribute> Attributes { get; set; }
	}

	[DataContract]
	public class ProductAttribute {
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public int ProductID { get; set; }
		[DataMember]
		public int AttributeID { get; set; }
		[DataMember]
		public string Value { get; set; }
	}

	[DataContract]
	public class Attribute {
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public string Name { get; set; }
	}

	[DataContract]
	public class Manufacturer {
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public string Name { get; set; }
	}

	[DataContract]
	public class ProductStatus {
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public string Name { get; set; }
	}

	[DataContract]
	public class OrderRequest {
		[DataMember]
		public int CustomerID { get; set; }
		[DataMember]
		public int AddressID { get; set; }
		[DataMember]
		public List<OrderProduct> Products { get; set; }
	}

	[DataContract]
	public class Order {
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public int CustomerID { get; set; }
		[DataMember]
		public int StatusID { get; set; }
		[DataMember]
		public DateTime Date { get; set; }
		[DataMember]
		public string FirstName { get; set; }
		[DataMember]
		public string MiddleName { get; set; }
		[DataMember]
		public string LastName { get; set; }
		[DataMember]
		public string Country { get; set; }
		[DataMember]
		public string City { get; set; }
		[DataMember]
		public string Address { get; set; }
		[DataMember]
		public string Postcode { get; set; }
		[DataMember]
		public List<OrderProduct> Products { get; set; }
	}

	[DataContract]
	public class OrderProduct {
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public int OrderID { get; set; }
		[DataMember]
		public int ProductID { get; set; }
		[DataMember]
		public decimal Price { get; set; }
		[DataMember]
		public int Quantity { get; set; }
	}

	[DataContract]
	public class OrderStatus {
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public string Name { get; set; }
	}

	[DataContract]
	public class Customer {
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public string FirstName { get; set; }
		[DataMember]
		public string MiddleName { get; set; }
		[DataMember]
		public string LastName { get; set; }
		[DataMember]
		public string Phone { get; set; }
		[DataMember]
		public string Email { get; set; }
		[DataMember]
		public List<CustomerAddress> Addresses { get; set; }
	}

	[DataContract]
	public class CustomerAddress {
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public int CustomerID { get; set; }
		[DataMember]
		public string Country { get; set; }
		[DataMember]
		public string City { get; set; }
		[DataMember]
		public string Address { get; set; }
		[DataMember]
		public string Postcode { get; set; }
	}
}
