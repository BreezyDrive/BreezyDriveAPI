using BreezyDrive.PaymentServices.Application.Interfaces;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;

namespace BreezyDrive.PaymentServices.Application.Services;

public class VnpayService (IVnpay vnpay) : IVnpayService
{
    
    
    // https://sandbox.vnpayment.vn/paymentv2/vpcpay.html?
    // vnp_Amount=1806000&vnp_Command=pay&vnp_CreateDate=20210801153333&vnp_CurrCode=VND&vnp_IpAddr=127.0.0.1&vnp_Locale=vn&vnp_OrderInfo=Thanh+toan+don+hang+%3A5&vnp_OrderType=other&vnp_ReturnUrl=https%3A%2F%2Fdomainmerchant.vn%2FReturnUrl&vnp_TmnCode=DEMOV210&vnp_TxnRef=5&vnp_Version=2.1.0&vnp_SecureHash=3e0d61a0c0534b2e36680b3f7277743e8784cc4e1d68fa7d276e79c23be7d6318d338b477910a27992f5057bb1582bd44bd82ae8009ffaf6d141219218625c42
    const string API_DOMAIN = "http://localhost:8080";

    public static string vnp_PayUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
    public static string vnp_ReturnUrl = API_DOMAIN + "/payment-callback";
    public static string vnp_TmnCode = "5KB5SZN9";
    public static string vnp_Version = "2.1.0";
    public static string vnp_IpAddr = "127.0.0.1";
    public static string vnp_Command = "pay";
    public static string secretKey = "DANFUDDWJFTGUTXLSTKHVSBNWNEIDHJB";
    public static string vnp_ApiUrl = "https://sandbox.vnpayment.vn/merchant_webapi/api/transaction";
    
    public string CreateRequestUrl()
    {
        var paymentRequest = new PaymentRequest
        {
            PaymentId = DateTime.Now.Ticks,
            Money = 20000,
            Description = "ahihi do ngoc",
            IpAddress = vnp_IpAddr,
            BankCode = BankCode.ANY, // Tùy chọn. Mặc định là tất cả phương thức giao dịch
            CreatedDate = DateTime.Now, // Tùy chọn. Mặc định là thời điểm hiện tại
            Currency = Currency.VND, // Tùy chọn. Mặc định là VND (Việt Nam đồng)
            Language = DisplayLanguage.Vietnamese // Tùy chọn. Mặc định là tiếng Việt
        };
        
        return vnpay.GetPaymentUrl(paymentRequest);
    }
}