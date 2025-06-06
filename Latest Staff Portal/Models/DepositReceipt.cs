namespace Latest_Staff_Portal.Models
{
    public class DepositReceipt
    {
        //  public string ODataEtag { get; set; }
        //public string No { get; set; }
        public string Date { get; set; }
        public string Receipt_Type { get; set; }

        public string Pay_Mode { get; set; }

        // public string Cheque_No { get; set; }
        //  public string Cheque_Date { get; set; }
        public decimal Amount { get; set; }

        // public decimal Amount_LCY { get; set; }
        // public string Bank_Code { get; set; }
        //public string Currency_Code { get; set; }
        // public string Received_From { get; set; }
        // public string On_Behalf_Of { get; set; }
        // public string Payment_Reference { get; set; }
        // public string Cashier { get; set; }
        //public DateTime Posted_Date { get; set; }
        //  public string Posted_Time { get; set; }
        // public string Posted_By { get; set; }
        public string Status { get; set; }
        public string Global_Dimension_1_Code { get; set; }
        public string Department_Name { get; set; }
        public string Global_Dimension_2_Code { get; set; }
        public string Project_Name { get; set; }
        public string PRN_No { get; set; }
        public string Case_No { get; set; }
        public string Case_Type { get; set; }
        public string Case_Title { get; set; }
        public bool Posted { get; set; }
    }
}