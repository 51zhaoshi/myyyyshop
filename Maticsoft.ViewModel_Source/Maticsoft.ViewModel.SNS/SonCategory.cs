namespace Maticsoft.ViewModel.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;

    public class SonCategory
    {
        public List<Categories> Grandson = new List<Categories>();
        public Categories ParentModel = new Categories();
    }
}

