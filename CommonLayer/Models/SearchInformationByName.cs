namespace InventoryManagement.CommonLayer.Models
{
    public class SearchInformationByNameRequest
    {
        public string Name { get; set; }
    }

    public class SearchInformationByNameResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ItemSearchInformationByName> itemsearchInformationByName { get; set; }
        //public SupplierSearchInformationByName suppliersearchinformationByName { get; set; }
        //public ItemCatSearchInformationByName itemcatsearchinformationByName { get; set; }
        
        public List<SubCatSearchInformationByCat> subcatsearchinformationByCat { get; set; }

    }
    public class ItemSearchInformationByName
    {
        public int ItemCode { get; set; }
        public string ItemName { get; set; }
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
        //public string CategoryName { get; set; }
        public string SubCatName { get; set; }
    }
}
