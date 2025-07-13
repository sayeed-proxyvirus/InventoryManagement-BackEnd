namespace InventoryManagement.CommonLayer.Models
{
    public class ReadInformationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ItemReadInformation> itemreadinformation { get; set;}
        public List<SupplierReadInformation> supplierreadinformation { get; set; }
        public List<ItemCatInformation> itemcatreadinformation { get; set; }
        public List<SubCatInformation> subcatreadinformation { get; set; }
        public List<ICReadInformation> icreadinformation { get; set; }
        public List<SupCReadInformation> supcreadinformation { get; set; }
        public List<ItemCatCReadInformation> itemcatcreadinformation { get; set; }
        public List<SubCatCReadInformation> subcatcreadinformation { get; set; }
    }



    public class ItemReadInformation
    {
        public int ItemCode { get; set; }
        public string ItemName { get; set; }
        public string CatName { get; set; }
        public string SubCatName { get; set; }
        public string Unit { get; set; }

        public int OpeningStock { get; set; }
        public int CurrentStock { get; set; }
        public int ReorderStock { get; set; }
        public int MaxStock { get; set; }

        public int LastPurRate { get; set; }
        public DateOnly LastPurDate { get; set; }

        public string PrefAlt { get; set; }
        public int Amount { get; set; }
        public string HScode { get; set; }

        public bool IsActive { get; set; }
        public DateOnly CreatedAT { get; set; }
        public DateOnly ExpiryDate { get; set; }
    }

    public class ICReadInformation 
    {
        public int Count { get; set; }
    }
    public class SupplierReadInformation
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
    public class SupCReadInformation
    {
        public int Count { get; set; }
    }

    public class ItemCatInformation
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

    public class ItemCatCReadInformation
    {
        public int Count { get; set; }
    }
    public class SubCatCReadInformation
    {
        public int Count { get; set; }
    }

    public class SubCatInformation
    {
        public int SubCatID { get; set; }
        public string CategoryName { get; set; }
        public string SubCatName { get; set; }
    }
}
