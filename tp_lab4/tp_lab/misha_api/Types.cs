﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace tp_lab.misha_api.types {
	class Dealer {
		public int id { get; set; }
		public string name { get; set; }
	}

	class Car {
		public int id { get; set; }
		public int dealer_id { get; set; }
		public Dealer dealer { get; set; }
		public int driver_id { get; set; }
		public Driver driver { get; set; }
		public int owner_id { get; set; }
		public Owner owner { get; set; }
		public int capacity { get; set; }
		public string model { get; set; }
	}

	class Transportation {
		public int id { get; set; }
		public int car_id { get; set; }
		public Car car { get; set; }
		public int organization_id { get; set; }
		public Organization organization { get; set; }
		public int store_id { get; set; }
		public Store store { get; set; }
		
		public DateTime date { get; set; }
	}

	class Shipment {
		public int id { get; set; }
		public int product_id { get; set; }
		public Product product { get; set; }
		public int transportation_id { get; set; }
		public Transportation transportation { get; set; }
		public int amount { get; set; }
	}

	class Product {
		public int id { get; set; }
		public int type_id { get; set; }
		public ProductType type { get; set; }
		public string name { get; set; }
		public int weight { get; set; }
	}

	class ProductType {
		public int id { get; set; }
		public string name { get; set; }
	}

	class Organization {
		public int id { get; set; }
		public string name { get; set; }
		public string address { get; set; }
	}

	class Store {
		public int id { get; set; }
		public int owner_id { get; set; }
		public Owner owner { get; set; }
		public string name { get; set; }
	}

	class Owner {
		public int id { get; set; }
		public string name { get; set; }
	}

	class Driver {
		public int id { get; set; }
		public string name { get; set; }
		public DateTime experience { get; set; }
		public int salary { get; set; }
	}
}
