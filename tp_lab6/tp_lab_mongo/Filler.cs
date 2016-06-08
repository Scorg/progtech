using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using common;
using MongoDB.Driver;

namespace tp_lab {
	class Filler {
		public static void CreateOrders(int count, int chunk)
		{
			try {
				List<int>   cust_ids,
							prod_ids,
							stat_ids;

				MongoContext ctx = new MongoContext();
				
				{
					cust_ids = ctx.Set<Customer>().AsQueryable().Select(x => x.id).ToList();
					prod_ids = ctx.Set<Product>().AsQueryable().Select(x => x.id).ToList();
					stat_ids = ctx.Set<ProductStatus>().AsQueryable().Select(x => x.id).ToList();
				}

				if (prod_ids.Count==0 || stat_ids.Count==0 || cust_ids.Count==0) return;

				Random r = new Random();

				for (int i=0,j=0; i<count; ++i, ++j) {
					if (j==chunk) {

						j = 0;
					}

					Customer c = ctx.Set<Customer>().Find(x => x.id == cust_ids[r.Next(cust_ids.Count)]).First();
					Address a = c.addresses.FirstOrDefault();

					if (a==null) {
						i--;
						j--;
						continue;
					}

					Order o = new Order();
					o.id = MongoCounter<Order>.NextValue();
					o.status_id = stat_ids[r.Next(stat_ids.Count)];
					o.date = DateTime.Now.AddMinutes(-r.Next(60*24*365));

					o.country = a.country;
					o.city = a.city;
					o.address = a.address;
					o.postcode = a.postcode;

					o.customer_id = c.id;
					o.first_name = c.first_name;
					o.middle_name = c.middle_name;
					o.last_name = c.last_name;
					
					int nprods = r.Next(1, prod_ids.Count);
					List<int> unused = prod_ids.ToList();

					for (int k=0; k<nprods; ++k) {
						OrderProduct op = new OrderProduct();
						op.id = MongoCounter<OrderProduct>.NextValue();
						op.order_id = o.id;
						op.product_id = unused[r.Next(unused.Count)];
						unused.Remove(op.product_id);
						op.price = ctx.Set<Product>().Find(x => x.id==op.product_id).First().price;
						op.quantity = r.Next(1, 6);
						o.products.Add(op);
					}

					ctx.Set<Order>().InsertOneAsync(o);
				}
			} catch (Exception ex) {
				ex.Log();
			}
		}
	}
}
