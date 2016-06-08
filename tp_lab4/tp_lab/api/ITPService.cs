using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Collections.Generic;

namespace tp_lab.api {
	[ServiceContract]
	interface ITPService {
		// -----------------------------------------------------
		// Количества
		// -----------------------------------------------------
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int GetAttributeCount();
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int GetCategoryCount();
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int GetCustomerCount();
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int GetCustomerAddressCount(int id);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int GetManufacturerCount();
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int GetOrderCount();
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int GetOrderProductCount(int order);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int GetProductCount();
		

		
		// -----------------------------------------------------
		// Получение
		// -----------------------------------------------------
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		types.Attribute GetAttribute(int id);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		types.Category GetCategory(int id);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		types.Customer GetCustomer(int id);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		types.CustomerAddress GetCustomerAddress(int id);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		types.Manufacturer GetManufacturer(int id);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		types.Order GetOrder(int id);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		types.OrderStatus GetOrderStatus(int id);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		types.Product GetProduct(int id);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		types.ProductStatus GetProductStatus(int id);



		// -----------------------------------------------------
		// Множественное получение
		// -----------------------------------------------------
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		List<types.Attribute> GetAttributes(int skip, int count);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		List<types.Category> GetCategories(int skip, int count);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		List<types.Customer> GetCustomers(int skip, int count, bool withAddresses = false);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		List<types.CustomerAddress> GetCustomerAddresses(int id);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		List<types.Manufacturer> GetManufacturers(int skip, int count);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		List<types.Order> GetOrders(int skip, int count, bool withProducts = false);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		List<types.OrderProduct> GetOrderProducts(int order);
		
		[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		List<types.Product> GetProducts(int skip, int count, bool withAttributes = false);

			

		// -----------------------------------------------------
		// Создание
		// -----------------------------------------------------
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int AddCustomerAddress(types.CustomerAddress value);
		
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int AddAttribute(types.Attribute value);
		
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int AddCategory(types.Category value);
		
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int AddCustomer(types.Customer value);
		
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int AddManufacturer(types.Manufacturer value);
		
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int AddOrder(types.OrderRequest value);
		
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int AddOrderStatus(types.OrderStatus value);
		
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int AddProduct(types.Product value);
		
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int AddProductAttribute(types.ProductAttribute v);
		
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		int AddProductStatus(types.ProductStatus value);


		
		// -----------------------------------------------------
		// Изменение
		// -----------------------------------------------------
		[WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		bool SetAttribute(types.Attribute value);
		
		[WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		bool SetCategory(types.Category value);
		
		[WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		bool SetCustomer(types.Customer value);
		
		[WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		bool SetCustomerAddress(types.CustomerAddress value);
		
		[WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		bool SetManufacturer(types.Manufacturer value);
		
		//[WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		//[OperationContract]
		//bool SetOrder(types.Order value);
		
		//[WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		//[OperationContract]
		//bool SetOrderProduct(types.OrderProduct value);
		
		[WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		bool SetOrderStatus(types.OrderStatus value);
		
		[WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		bool SetProduct(types.Product value);
		
		[WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		bool SetProductAttribute(types.ProductAttribute value);
		
		[WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json )]
		[OperationContract]
		bool SetProductStatus(types.ProductStatus value);
	}
}
