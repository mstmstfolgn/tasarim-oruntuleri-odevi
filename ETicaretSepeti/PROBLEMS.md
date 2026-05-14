BENÝM YORUMUM:
 
1. Tek Sorumluluk Prensibinin (SRP) Ýhlali
ShoppingCart sýnýfý ve içindeki CalculateTotal metodu tüm iþlemleri ayný çatý altýnda yapmaya çalýþýyor. 
Ürün ekleme, indirim hesaplama, kupon kontrolü ve kargo iþlemleri tek bir sýnýfta toplanmýþ.
Bu durum kodun bakýmýný zorlaþtýrýr.

2. Açýk/Kapalý Prensibinin (OCP) Ýhlali (if-else Zincirleri)
Çok fazla if-else yapýsý var. Gelecek zamanda kampanya deðiþikliði ya da ödeme yöntemi eklenmesi gerektiðinde, 
mevcut kodu deðiþtirmek zorunda kalacaðýz. Bakým zorluklarý ve hata yapma riski artacak. Kodun esnekliðini azaltýr.

3.Ýlkel Tip Takýntýsý (Primitive Obsession)
Müþteri tipi ("VIP"), para birimi ("USD"), kategori ("Electronics") gibi kritik verilerin hepsi  metin olarak tutuluyor.
Bir yazýlýmcý yanlýþlýkla "VIP" yerine "VÝP" yazarsa bütün indirim sistemi çöker ve hata da vermez. 
Bunlarýn enum veya kendi baþlarýna birer class olmasý gerekirdi.

4. Sabit Kodlanmýþ (Hardcoded) Deðerler
Yaþ, müþteri tipi ve indirim oranlarý gibi deðerler doðrudan kodun içine yazýlmýþ. Anlýk deðiþen dolarý ben koda girip mi deðiþtireceðim?
dinamik þekilde çekmek lazým.

5. Kendini Tekrar Etme (DRY - Don't Repeat Yourself) Ýhlali
Döviz çevirme iþlemine bakarsan, döviz kuru çarpým mantýðý metodun baþýnda bir kez ürünler için yapýlýyor,
sonra metodun en sonunda tekrar toplam tutarý çevirmek için bir daha yazýlmýþ (TargetCurrency).
Ýleride Euro kurunu deðiþtirecek bir yazýlýmcý, yukarýdaki kuru güncelleyip aþaðýdakini unutursa þirket binlerce lira zarar edebilir.


AI YORUMU:

SRP Ýhlali / God Class
ShoppingCart sýnýfý çok fazla iþi ayný anda yapýyor. Bu durum kodun bakýmýný ve geliþtirilmesini zorlaþtýrýyor.
Çözüm: Strategy Pattern

God Method Sorunu
CalculateTotal() metodu aþýrý büyümüþ ve bütün iþlemleri tek baþýna yapýyor. Okunmasý ve hata ayýklamasý zor hale geliyor.
Çözüm: Chain of Responsibility

OCP Ýhlali
Yeni müþteri tipi veya kampanya eklemek için mevcut kod deðiþtiriliyor. Bu durum hata riskini artýrýyor.
Çözüm: Strategy Pattern

Uzun if-else Zincirleri
Kodun birçok yerinde uzun if-else bloklarý kullanýlmýþ. Kod zamanla karmaþýk hale gelir.
Çözüm: Factory + Strategy Pattern

Hardcoded Deðerler
Ýndirim oranlarý ve döviz kurlarý doðrudan kod içine yazýlmýþ. Deðiþiklik yapmak zorlaþýyor.
Çözüm: Constant veya Configuration yapýsý

String Baðýmlýlýðý
"VIP" veya "USD" gibi string deðerler hata yapmaya açýktýr. Küçük yazým hatalarý sistemi bozabilir.
Çözüm: Enum kullanýmý

UI ile Ýþ Mantýðýnýn Karýþmasý
Console.WriteLine() hesaplama kodlarýnýn içinde kullanýlmýþ. Kodun katmanlý yapýsý bozulmuþ.
Çözüm: MVC yaklaþýmý

Yüksek Baðýmlýlýk
Bütün iþlemler ayný deðiþken üzerinden ilerliyor. Bir deðiþiklik baþka yerleri bozabilir.
Çözüm: Chain of Responsibility

Kural Karmaþasý
Vergi, kargo ve indirim kurallarý kod içine karýþýk þekilde yazýlmýþ. Yönetmesi zorlaþýyor.
Çözüm: Specification Pattern

Düþük Test Edilebilirlik
Kod çok iç içe olduðu için test yazmak zorlaþýyor. Parçalý yapý olmadýðý için kontrol etmek güçleþiyor.
Çözüm: Dependency Injection


BEN-AI karþýlaþtýrmasý:(ben 5 tane yazdým ai 10 tane buldu.)

Ýki yorum da SRP/OCP ihlali, hardcoded deðerler ve if-else karmaþasý gibi temel tasarým problemlerini fark etmiþ.
Ýkisi de kodun bakýmýnýn zorlaþacaðýný ve hata riskinin artacaðýný söylüyor.

Benim yorumum daha çok problemin neden kötü olduðunu açýklýyor. 
AI yorumu ise daha teknik isimler ve tasarým örüntüleri (Strategy Pattern, Factory vb.) öneriyor.

Benim yorumda “Primitive Obsession” ve “DRY ihlali” gibi daha detaylý tasarým problemleri var. AI yorumunda bunlar yok.

AI yorumunda ise ekstra olarak “God Method”, “UI ile iþ mantýðýnýn karýþmasý”,
“yüksek baðýmlýlýk” ve “düþük test edilebilirlik” gibi mimari problemler belirtilmiþ.

Benim yorum daha doðal ve yorumlama aðýrlýklý durmuþ. AI yorumu daha akademik ve teknik durmuþ.
Genel olarak benim yorum problem analizi kýsmýnda, AI yorumu ise çözüm ve tasarým örüntüsü kýsmýnda daha güçlü.


