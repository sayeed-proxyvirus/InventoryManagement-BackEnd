

using InventoryManagement.CommonLayer.Models;

namespace InventoryManagement.ServiceLayer
{
    public interface IINVApplicationSL
    {
        public Task<LoginResponse> Login(LoginRequest request);
        public Task<RegisterResponse> Register(RegisterRequest request);
        public Task<CreateInformationResponse> CreateInformation(IItemCreateInformationRequest request);
        public Task<CreateInformationResponse> CreateInformation(ItemTransCreateInformationRequest request);
        public Task<CreateInformationResponse> CreateInformation(GDCreateInformationRequest request);
        public Task<CreateInformationResponse> CreateInformation(SupplierInfoCreateInformationRequest request);
        public Task<CreateInformationResponse> CreateInformation(ItemCatCreateInfoCreateInformationRequest request);
        public Task<CreateInformationResponse> CreateInformation(SubCatCreateInfoCreateInformationRequest request);



        public Task<DeleteInformationResponse> IItemDeleteInfo(DeleteInformationRequest request);
        public Task<DeleteInformationResponse> GDDeleteInfo(DeleteInformationRequest request);
        public Task<DeleteInformationResponse> SupplierDeleteInfo(DeleteInformationRequest request);
        public Task<DeleteInformationResponse> ItemCatDeleteInfo(DeleteInformationRequest request);
        public Task<DeleteInformationResponse> SubCatDeleteInfo(DeleteInformationRequest request);





        public Task<ReadInformationResponse> ItemReadInformation();
        public Task<ReadInformationResponse> ICReadInformation();
        public Task<ReadInformationResponse> ItemTransReadInformation();
        public Task<ReadInformationResponse> ITCReadInformation();
        public Task<ReadInformationResponse> GDReadInformation();
        public Task<ReadInformationResponse> GDCReadInformation();
        public Task<ReadInformationResponse> SupCReadInformation();
        public Task<ReadInformationResponse> ItemCatCReadInformation();
        public Task<ReadInformationResponse> SubCatCReadInformation();
        public Task<ReadInformationResponse> SubCatInformation();
        public Task<ReadInformationResponse> SupplierReadInformation();
        public Task<ReadInformationResponse> ItemCatInformation();



        public Task<UpdateInformationResponse> UpdateInformation(ItemUpdateInformationRequest request);
        public Task<UpdateInformationResponse> UpdateInformation(ItemTransUpdateInformationRequest request);
        public Task<UpdateInformationResponse> UpdateInformation(GDUpdateInformationRequest request);
        public Task<UpdateInformationResponse> UpdateInformation(SupplierUpdateInformationRequest request);
        public Task<UpdateInformationResponse> UpdateInformation(ItemCatUpdateInformationRequest request);
        public Task<UpdateInformationResponse> UpdateInformation(SubCatUpdateInformationRequest request);



        public Task<SearchInformationByNameResponse> ItemSearchInformationByCat(SearchInformationByNameRequest request);
        public Task<SearchInformationByNameResponse> SubCatSearchInformationByCat(SearchInformationByNameRequest request);



    }
}
