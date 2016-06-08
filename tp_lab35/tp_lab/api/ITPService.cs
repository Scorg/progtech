using System;
using System.ServiceModel;
using System.Collections.Generic;

namespace tp_lab.api {
	[ServiceContract]
	interface ITPService {
		// -----------------------------------------------------
		// Количества
		// -----------------------------------------------------
		[OperationContract]
		int GetAttributeCount();

		[OperationContract]
		int GetCategoryCount();

		[OperationContract]
		int GetCustomerCount();

		[OperationContract]
		int GetCustomerAddressCount(int id);

		[OperationContract]
		int GetManufacturerCount();

		[OperationContract]
		int GetOrderCount();

		[OperationContract]
		int GetOrderProductCount(int order);

		[OperationContract]
		int GetProductCount();
		

		
		// -----------------------------------------------------
		// Получение
		// -----------------------------------------------------
		[OperationContract]
		types.Attribute GetAttribute(int id);
		
		[OperationContract]
		types.Category GetCategory(int id);
		
		[OperationContract]
		types.Customer GetCustomer(int id);
		
		[OperationContract]
		types.CustomerAddress GetCustomerAddress(int id);

		[OperationContract]
		types.Manufacturer GetManufacturer(int id);
		
		[OperationContract]
		types.Order GetOrder(int id);

		[OperationContract]
		types.OrderStatus GetOrderStatus(int id);
		
		[OperationContract]
		types.Product GetProduct(int id);

		[OperationContract]
		types.ProductStatus GetProductStatus(int id);



		// -----------------------------------------------------
		// Множественное получение
		// -----------------------------------------------------
		[OperationContract]
		List<types.Attribute> GetAttributes();

		[OperationContract]
		List<types.Category> GetCategories();
		
		[OperationContract]
		List<types.Customer> GetCustomers(int skip, int count);
		
		[OperationContract]
		List<types.CustomerAddress> GetCustomerAddresses(int id);
		
		[OperationContract]
		List<types.Manufacturer> GetManufacturers();
		
		[OperationContract]
		List<types.Order> GetOrders(int skip, int count);

		[OperationContract]
		List<types.OrderProduct> GetOrderProducts(int order);
		
		[OperationContract]
		List<types.Product> GetProducts(int skip, int count);


		
		// -----------------------------------------------------
		// Создание
		// -----------------------------------------------------
		[OperationContract]
		int AddCustomerAddress(types.CustomerAddress value);

		[OperationContract]
		int AddAttribute(types.Attribute value);

		[OperationContract]
		int AddCategory(types.Category value);
		
		[OperationContract]
		int AddCustomer(types.Customer value);
		
		[OperationContract]
		int AddManufacturer(types.Manufacturer value);
		
		[OperationContract]
		int AddOrder(types.OrderRequest value);

		[OperationContract]
		int AddOrderStatus(types.OrderStatus value);
		
		[OperationContract]
		int AddProduct(types.Product value);

		[OperationContract]
		int AddProductAttribute(types.ProductAttribute v);

		[OperationContract]
		int AddProductStatus(types.ProductStatus value);


		
		// -----------------------------------------------------
		// Изменение
		// -----------------------------------------------------
		[OperationContract]
		bool SetAttribute(types.Attribute value);

		[OperationContract]
		bool SetCategory(types.Category value);
		
		[OperationContract]
		bool SetCustomer(types.Customer value);
		
		[OperationContract]
		bool SetCustomerAddress(types.CustomerAddress value);
		
		[OperationContract]
		bool SetManufacturer(types.Manufacturer value);
		
		//[OperationContract]
		//bool SetOrder(types.Order value);
		
		//[OperationContract]
		//bool SetOrderProduct(types.OrderProduct value);
		
		[OperationContract]
		bool SetOrderStatus(types.OrderStatus value);
		
		[OperationContract]
		bool SetProduct(types.Product value);
		
		[OperationContract]
		bool SetProductAttribute(types.ProductAttribute value);
		
		[OperationContract]
		bool SetProductStatus(types.ProductStatus value);
	}
}
