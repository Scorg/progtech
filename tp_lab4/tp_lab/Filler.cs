using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using common;

namespace tp_lab {
	class Filler {
		public static void CreateOrders(int count, int chunk)
		{
			try {
				List<int> cust_ids,
							  prod_ids,
							  stat_ids;

				using (Entities c = new Entities()) {
					c.Configuration.AutoDetectChangesEnabled = false;


					cust_ids = c.customer.Select(x => x.id).ToList();
					prod_ids = c.product.Select(x => x.id).ToList();
					stat_ids = c.order_status.Select(x => x.id).ToList();
				}

				if (prod_ids.Count==0 || stat_ids.Count==0 || cust_ids.Count==0) return;

				Entities ctx = new Entities();
						ctx.Configuration.AutoDetectChangesEnabled = false;

						Random r = new Random();

				for (int i=0,j=0; i<count; ++i, ++j) {
					if (j==chunk) {
						ctx.ChangeTracker.DetectChanges();
						ctx.SaveChanges();
						ctx.Dispose();

						ctx = new Entities();
						ctx.Configuration.AutoDetectChangesEnabled = false;
						j = 0;
					}

					Customer c = ctx.customer.Find(cust_ids[r.Next(cust_ids.Count-1)]);
					Address a = c.addresses.FirstOrDefault();

					if (a==null) {
						i--;
						j--;
						continue;
					}

					Order o = ctx.order.Create();
					o.status_id = stat_ids[r.Next(stat_ids.Count-1)];
					o.date = DateTime.Now.AddMinutes(-r.Next(60*24*365));

					o.country = a.country;
					o.city = a.city;
					o.address = a.address;
					o.postcode = a.postcode;

					o.customer = c;
					o.first_name = c.first_name;
					o.middle_name = c.middle_name;
					o.last_name = c.last_name;
					
					int nprods = r.Next(1, prod_ids.Count-1);
					List<int> unused = prod_ids.ToList();

					for (int k=0; k<nprods; ++k) {
						OrderProduct op = ctx.order_product.Create();
						op.product_id = unused[r.Next(unused.Count-1)];
						unused.Remove(op.product_id);
						op.price = ctx.product.Find(op.product_id).price;
						op.quantity = r.Next(1, 6);
						o.products.Add(op);
					}

					ctx.order.Add(o);
				}
			} catch (Exception ex) {
				ex.Log();
			}
		}
	}
}
