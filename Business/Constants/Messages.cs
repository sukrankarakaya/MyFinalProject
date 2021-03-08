using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public class Messages
    {
            public static string ProductAdded = "Ürün eklendi.";
            public static string ProductNameInvalid = "Ürün ismi geçersiz.";
            public static string MaintenanceTime = "Sistem bakımda";
            public static string ProductsListed="Ürünler listelendi";
            public static string ProductCountOfCategoryError ="Bir kategoride en fazla 10 ürün olabilir.";
            public static string ProductNameAlreadyExistsr = "Bu isinde bir ürün var.";
            public static string CategoryLimitExceded = "Kategory sınırı aşıldıgı için yeni ürün eklenemiyor.";
            public static string AuthorizationDenied = "Yetkiniz yok.";
            public static string UserRegistered ="Kayıt oluştu.";
            public static string UserAlreadyExists ="Kullanıcı mevcut.";
            public static string AccessTokenCreated = "Token oluşturuldu. oluştu.";
    }
}
