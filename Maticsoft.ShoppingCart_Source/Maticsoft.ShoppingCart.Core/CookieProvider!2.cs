namespace Maticsoft.ShoppingCart.Core
{
    using Maticsoft.Common.DEncrypt;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Collections.Generic;
    using System.Web;

    public class CookieProvider<TCartInfo, TCartItemInfo> : ICartProvider<TCartInfo, TCartItemInfo> where TCartInfo: CartInfo<TCartItemInfo>, new() where TCartItemInfo: CartItemInfo, new()
    {
        protected const string BASE_COOKIE_INDEXKEY = "maticsoft_shoppingcart_index_{0}";
        protected readonly string CartDataCookieDataKey;
        protected readonly string CartDataCookieIndexKey;
        protected readonly double CookieExpiresDayNum;
        protected const int CookieItemsMaxNum = 10;
        protected const int CookieKeyMaxNum = 15;
        protected const int NOLOGIN_USERID = -1;
        protected readonly int UserId;

        public CookieProvider(int userId)
        {
            this.CookieExpiresDayNum = 1.0;
            this.UserId = userId;
            this.CartDataCookieIndexKey = string.Format("maticsoft_shoppingcart_index_{0}", userId);
            this.CartDataCookieDataKey = string.Format("maticsoft_shoppingcart_data_{0}", userId + "_{0}");
        }

        public virtual void AddItem(TCartItemInfo itemInfo)
        {
            if (itemInfo != null)
            {
                if (itemInfo.Quantity <= 0)
                {
                    itemInfo.Quantity = 1;
                }
                TCartInfo shoppingCart = this.GetShoppingCart();
                TCartItemInfo local2 = shoppingCart[itemInfo.SKU];
                if (local2 != null)
                {
                    local2.Quantity++;
                }
                else
                {
                    itemInfo.ItemId = this.GenerateLastItemId(shoppingCart.Items.Count);
                    shoppingCart.Items.Add(itemInfo);
                }
                this.SaveShoppingCart(shoppingCart);
            }
        }

        public virtual void ClearShoppingCart()
        {
            HttpCookie cookieIndex = CookieProvider<TCartInfo, TCartItemInfo>.GetCookie(this.CartDataCookieIndexKey);
            if (cookieIndex != null)
            {
                this.ResolveCartItemCookie(cookieIndex, delegate (HttpCookie itemCookie) {
                    HttpCookie cookie = new HttpCookie(itemCookie.Name) {
                        Expires = DateTime.Now.AddDays(-1.0)
                    };
                    itemCookie = cookie;
                    HttpContext.Current.Response.Cookies.Set(itemCookie);
                });
                HttpCookie cookie2 = new HttpCookie(cookieIndex.Name) {
                    Expires = DateTime.Now.AddDays(-1.0)
                };
                cookieIndex = cookie2;
                HttpContext.Current.Response.Cookies.Set(cookieIndex);
            }
        }

        protected virtual int GenerateLastItemId(int count)
        {
            if (count < 1)
            {
                return 1;
            }
            return ++count;
        }

        public static HttpCookie GetCookie(string name)
        {
            foreach (string str in HttpContext.Current.Response.Cookies.AllKeys)
            {
                if (str == name)
                {
                    return HttpContext.Current.Response.Cookies[str];
                }
            }
            foreach (string str2 in HttpContext.Current.Request.Cookies.AllKeys)
            {
                if (str2 == name)
                {
                    return HttpContext.Current.Request.Cookies[str2];
                }
            }
            return null;
        }

        public virtual TCartInfo GetShoppingCart()
        {
            HttpCookie cookie = CookieProvider<TCartInfo, TCartItemInfo>.GetCookie(this.CartDataCookieIndexKey);
            return this.GetShoppingCart(cookie);
        }

        public virtual TCartInfo GetShoppingCart(HttpCookie cookie)
        {
            if ((cookie == null) || string.IsNullOrEmpty(cookie.Value))
            {
                return Activator.CreateInstance<TCartInfo>();
            }
            TCartInfo shoppingCart = Activator.CreateInstance<TCartInfo>();
            shoppingCart.Items = new List<TCartItemInfo>();
            this.ResolveCartItemCookie(cookie, delegate (HttpCookie itemCookie) {
                string str = itemCookie.Value;
                if (!string.IsNullOrWhiteSpace(str))
                {
                    List<TCartItemInfo> list;
                    try
                    {
                        list = (List<TCartItemInfo>) JsonConvert.Import<IList<TCartItemInfo>>(GZip.DeflateDecompress(HttpUtility.UrlDecode(str)));
                    }
                    catch
                    {
                        throw;
                    }
                    if ((list != null) && (list.Count >= 1))
                    {
                        list.ForEach(delegate (TCartItemInfo xx) {
                            shoppingCart.Items.Add(xx);
                        });
                    }
                }
            });
            return shoppingCart;
        }

        public static void LoadShoppingCart(int userId)
        {
            if (userId >= 1)
            {
                HttpCookie cookie = CookieProvider<TCartInfo, TCartItemInfo>.GetCookie(string.Format("maticsoft_shoppingcart_index_{0}", -1));
                if ((cookie != null) && !string.IsNullOrWhiteSpace(cookie.Value))
                {
                    CookieProvider<TCartInfo, TCartItemInfo> provider = new CookieProvider<TCartInfo, TCartItemInfo>(-1);
                    CookieProvider<TCartInfo, TCartItemInfo> provider2 = new CookieProvider<TCartInfo, TCartItemInfo>(userId);
                    TCartInfo shoppingCart = provider.GetShoppingCart();
                    if ((shoppingCart.Items != null) && (shoppingCart.Items.Count > 0))
                    {
                        provider2.SaveShoppingCart(shoppingCart);
                    }
                    provider.ClearShoppingCart();
                }
            }
        }

        public virtual void RemoveItem(int itemId)
        {
            TCartInfo shoppingCart = this.GetShoppingCart();
            if ((shoppingCart.Items != null) && (shoppingCart.Items.Count >= 1))
            {
                shoppingCart.Items.RemoveAll(xx => xx.ItemId == itemId);
                this.SaveShoppingCart(shoppingCart);
            }
        }

        protected virtual void ResolveCartItemCookie(HttpCookie cookieIndex, Action<HttpCookie> methodProcessCartItem)
        {
            if (cookieIndex != null)
            {
                string str = GZip.DeflateDecompress(HttpUtility.UrlDecode(cookieIndex.Value));
                if (!string.IsNullOrWhiteSpace(str))
                {
                    string[] strArray = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (strArray.Length >= 1)
                    {
                        foreach (string str2 in strArray)
                        {
                            if (!string.IsNullOrWhiteSpace(str2))
                            {
                                HttpCookie cookie = CookieProvider<TCartInfo, TCartItemInfo>.GetCookie(str2);
                                if (cookie != null)
                                {
                                    methodProcessCartItem(cookie);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void SaveShoppingCart(TCartInfo cartInfo)
        {
            if (((cartInfo == null) || (cartInfo.Items == null)) || (cartInfo.Items.Count < 0))
            {
                this.ClearShoppingCart();
            }
            else
            {
                if (150 < cartInfo.Items.Count)
                {
                    throw new IndexOutOfRangeException("SaveShoppingCart: MaxCount");
                }
                int num = 1;
                int num2 = cartInfo.Items.Count - 1;
                List<TCartItemInfo> list = new List<TCartItemInfo>();
                List<string> values = new List<string>();
                string item = string.Format(this.CartDataCookieDataKey, num++);
                values.Add(item);
                HttpCookie cookie = new HttpCookie(item);
                for (int i = 0; i < cartInfo.Items.Count; i++)
                {
                    if ((num > 0) && ((num % 10) == 0))
                    {
                        cookie.Value = HttpUtility.UrlEncode(GZip.DeflateCompress(JsonConvert.ExportToString(list)));
                        cookie.Expires = DateTime.Now.AddDays(this.CookieExpiresDayNum);
                        HttpContext.Current.Response.Cookies.Set(cookie);
                        if (i < num2)
                        {
                            values.Add(string.Format(this.CartDataCookieDataKey, num));
                            cookie = new HttpCookie(string.Format(this.CartDataCookieDataKey, num++));
                            list.Clear();
                        }
                    }
                    list.Add(cartInfo.Items[i]);
                }
                cookie.Value = HttpUtility.UrlEncode(GZip.DeflateCompress(JsonConvert.ExportToString(list)));
                cookie.Expires = DateTime.Now.AddDays(this.CookieExpiresDayNum);
                HttpContext.Current.Response.Cookies.Set(cookie);
                HttpCookie cookie2 = new HttpCookie(this.CartDataCookieIndexKey) {
                    Value = HttpUtility.UrlEncode(GZip.DeflateCompress(string.Join(",", values))),
                    Expires = DateTime.Now.AddDays(this.CookieExpiresDayNum)
                };
                HttpContext.Current.Response.Cookies.Set(cookie2);
            }
        }

        public virtual void UpdateItemQuantity(int itemId, int quantity)
        {
            Action<TCartItemInfo> action = null;
            if (quantity <= 0)
            {
                this.RemoveItem(itemId);
            }
            else
            {
                TCartInfo shoppingCart = this.GetShoppingCart();
                if ((shoppingCart.Items != null) && (shoppingCart.Items.Count >= 1))
                {
                    if (action == null)
                    {
                        action = delegate (TCartItemInfo xx) {
                            if (xx.ItemId == itemId)
                            {
                                xx.Quantity = quantity;
                            }
                        };
                    }
                    shoppingCart.Items.ForEach(action);
                    this.SaveShoppingCart(shoppingCart);
                }
            }
        }
    }
}

