namespace Maticsoft.Web
{
    using System;
    using System.Collections;
    using System.Web;

    public class SingleLogin
    {
        private readonly string appKey = "online";
        private readonly string logout = "{$}";

        public bool IsLogin(object id)
        {
            Hashtable hashtable = (Hashtable) HttpContext.Current.Application[this.appKey];
            if (hashtable != null)
            {
                IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Value.ToString().Equals(id.ToString()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void UserLogin(object id)
        {
            Hashtable hashtable = (Hashtable) HttpContext.Current.Application[this.appKey];
            if (hashtable == null)
            {
                hashtable = new Hashtable();
            }
            IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value.ToString().Equals(id.ToString()))
                {
                    hashtable[enumerator.Key.ToString()] = this.logout;
                    break;
                }
            }
            hashtable[HttpContext.Current.Session.SessionID] = id.ToString();
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application[this.appKey] = hashtable;
            HttpContext.Current.Application.UnLock();
        }

        public bool ValidateForceLogin()
        {
            bool flag = false;
            Hashtable hashtable = (Hashtable) HttpContext.Current.Application[this.appKey];
            if (hashtable != null)
            {
                IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Key.ToString().Equals(HttpContext.Current.Session.SessionID))
                    {
                        if (enumerator.Value.ToString().Equals(this.logout))
                        {
                            hashtable.Remove(HttpContext.Current.Session.SessionID);
                            HttpContext.Current.Application.Lock();
                            HttpContext.Current.Application[this.appKey] = hashtable;
                            HttpContext.Current.Application.UnLock();
                            HttpContext.Current.Session.RemoveAll();
                            flag = true;
                        }
                        return flag;
                    }
                }
            }
            return flag;
        }
    }
}

