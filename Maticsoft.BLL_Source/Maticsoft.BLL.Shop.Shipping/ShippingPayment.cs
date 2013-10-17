namespace Maticsoft.BLL.Shop.Shipping
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Shipping;
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ShippingPayment
    {
        private readonly IShippingPayment dal = DAShopShipping.CreateShippingPayment();

        public bool Add(Maticsoft.Model.Shop.Shipping.ShippingPayment model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Shipping.ShippingPayment> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Shipping.ShippingPayment> list = new List<Maticsoft.Model.Shop.Shipping.ShippingPayment>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Shipping.ShippingPayment item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int modeId)
        {
            return this.dal.Delete(modeId);
        }

        public bool Delete(int ShippingModeId, int PaymentModeId)
        {
            return this.dal.Delete(ShippingModeId, PaymentModeId);
        }

        public bool Exists(int ShippingModeId, int PaymentModeId)
        {
            return this.dal.Exists(ShippingModeId, PaymentModeId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Shop.Shipping.ShippingPayment GetModel(int ShippingModeId, int PaymentModeId)
        {
            return this.dal.GetModel(ShippingModeId, PaymentModeId);
        }

        public Maticsoft.Model.Shop.Shipping.ShippingPayment GetModelByCache(int ShippingModeId, int PaymentModeId)
        {
            string cacheKey = "ShippingPaymentModel-" + ShippingModeId + PaymentModeId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ShippingModeId, PaymentModeId);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.Shop.Shipping.ShippingPayment) cache;
        }

        public List<Maticsoft.Model.Shop.Shipping.ShippingPayment> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Shipping.ShippingPayment model)
        {
            return this.dal.Update(model);
        }
    }
}

