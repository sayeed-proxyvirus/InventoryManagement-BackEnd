namespace InventoryManagement.CommonLayer.Models
{
    public class SearchInformationByNameRequest
    {
        public int ID { get; set; }
    }

    public class SearchInformationByNameResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ItemSearchInformationByCat> itemsearchInformationByCat { get; set; }
        //public SupplierSearchInformationByName suppliersearchinformationByName { get; set; }
        //public ItemCatSearchInformationByName itemcatsearchinformationByName { get; set; }
        
        public List<SubCatSearchInformationByCat> subcatsearchinformationByCat { get; set; }

    }
    public class ItemSearchInformationByCat
    {
        public int ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string CatName { get; set; }
        public string SubCatName { get; set; }
        //public string Unit { get; set; }

        public int OpeningStock { get; set; }
        public int ReorderStock { get; set; }
        public int MaxStock { get; set; }

        public string PrefAlt { get; set; }
        public string HScode { get; set; }

        public bool IsActive { get; set; }
        public DateOnly CreatedAT { get; set; }
    }
        //public class SupplierSearchInformationByName
        //{
        //    public int SupplierID { get; set; }
        //    //public string SupplierName { get; set; }
        //    public string ContactNumber { get; set; }
        //    public string Email { get; set; }
        //    public string Address { get; set; }
        //}
        //public class ItemCatSearchInformationByName
        //{
        //    public int CategoryID { get; set; }
        //    public string CategoryName { get; set; }
        //}
        public class SubCatSearchInformationByCat
    {
        public int SubCatID { get; set; }
        public int CategoryID { get; set; }
        public string SubCatName { get; set; }
    }
}
