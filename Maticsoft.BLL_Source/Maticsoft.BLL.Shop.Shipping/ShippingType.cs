namespace Maticsoft.BLL.Shop.Shipping
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Shipping;
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class ShippingType
    {
        private readonly IShippingType dal = DAShopShipping.CreateShippingType();

        public int Add(Maticsoft.Model.Shop.Shipping.ShippingType model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Shipping.ShippingType> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Shipping.ShippingType> list = new List<Maticsoft.Model.Shop.Shipping.ShippingType>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Shipping.ShippingType item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ModeId)
        {
            return this.dal.Delete(ModeId);
        }

        public bool DeleteList(string ModeIdlist)
        {
            return this.dal.DeleteList(ModeIdlist);
        }

        public bool Exists(int ModeId)
        {
            return this.dal.Exists(ModeId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public decimal GetFreight(Maticsoft.Model.Shop.Shipping.ShippingType typeModel, int weight)
        {
            if (weight <= typeModel.Weight)
            {
                return typeModel.Price;
            }
            if ((!typeModel.AddWeight.HasValue || (typeModel.AddWeight.Value <= 0)) || (!typeModel.AddPrice.HasValue || (typeModel.AddPrice.Value < 0M)))
            {
                return typeModel.Price;
            }
            int num = weight - typeModel.Weight;
            int num3 = num;
            int num2 = ((num3 % typeModel.AddWeight) == 0) ? (num / typeModel.AddWeight.Value) : ((num / typeModel.AddWeight.Value) + 1);
            return (typeModel.Price + (num2 * typeModel.AddPrice.Value));
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

        public List<Maticsoft.Model.Shop.Shipping.ShippingType> GetListByPay(int paymentModeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("EXISTS ( SELECT ShippingModeId\r\n                 FROM   Shop_ShippingPayment\r\n                 WHERE  ShippingModeId = Shop_ShippingType.ModeId\r\n                        AND PaymentModeId = {0} )", paymentModeId);
            return this.GetModelList(builder.ToString());
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Shop.Shipping.ShippingType GetModel(int ModeId)
        {
            return this.dal.GetModel(ModeId);
        }

        public Maticsoft.Model.Shop.Shipping.ShippingType GetModelByCache(int ModeId)
        {
            string cacheKey = "ShippingTypeModel-" + ModeId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ModeId);
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
            return (Maticsoft.Model.Shop.Shipping.ShippingType) cache;
        }

        public List<Maticsoft.Model.Shop.Shipping.ShippingType> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Shipping.ShippingType model)
        {
            return this.dal.Update(model);
        }
    }
}

