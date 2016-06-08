using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_lab.api {
	partial class TPService: ITPService {		
		public int GetAttributeCount()
		{
			using (Entities ctx=new Entities()) {
				try {
					return ctx.attribute.Count();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}

		public int GetCategoryCount()
		{
			using (Entities ctx=new Entities()) {
				try {
					return ctx.category.Count();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}

		public int GetCustomerCount()
		{
			using (Entities ctx=new Entities()) {
				try {
					return ctx.customer.Count();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}

		public int GetCustomerAddressCount(int id)
		{
			using (Entities ctx=new Entities()) {
				try {					
					return ctx.address.Count(x => x.customer_id == id);
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}

		public int GetManufacturerCount()
		{
			using (Entities ctx=new Entities()) {
				try {					
					return ctx.manufacturer.Count();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}

		public int GetOrderCount()
		{
			using (Entities ctx=new Entities()) {
				try {					
					return ctx.order.Count();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}

		public int GetOrderProductCount(int order)
		{
			using (Entities ctx=new Entities()) {
				try {					
					return ctx.order_product.Count(x => x.order_id == order);
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}

		public int GetProductCount()
		{
			using (Entities ctx=new Entities()) {
				try {					
					return ctx.product.Count();
				} catch (Exception ex) {
					common.LogFile.Log(ex);
				}

				return 0;
			}
		}
	}
}
