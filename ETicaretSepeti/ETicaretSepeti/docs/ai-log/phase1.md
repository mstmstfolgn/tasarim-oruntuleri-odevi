AI’a ne sordum (Prompt):
“Elimde çok karışmış bir e-ticaret sepet sistemi var. Ürün oluşturma işlemleri her yerde dağılmış durumda
ve yeni ürün ekledikçe kod büyüyor. Bu sorunu çözmek için hangi yaratım (Creational) tasarım 
örüntüsünü kullanabilirim, mantığıyla birlikte açıklar mısın?”

AI ne cevap verdi (Özet):
AI, bu tarz durumlarda Factory Method kullanımının uygun olduğunu söyledi.
Nesne oluşturma işlemlerini tek bir merkezde toplamanın kodu daha düzenli hale getireceğini anlattı.
Ayrıca yeni ürün eklenirken mevcut kodu sürekli değiştirmemek için bunun OCP prensibine daha uygun olduğunu belirtti.

Ben ne yaptım:
AI’ın önerdiği mantığı kendi koduma göre uyarladım. Ürün oluşturma işlemlerini
UrunFactory isimli ayrı bir sınıfa taşıdım. Böylece Main içinde sürekli new Elektronik(), 
new Kitap() gibi nesneler oluşturmak yerine tek bir yerden üretim yapılmış oldu.

Ayrıca kodu tamamen olduğu gibi almadım. Sınıf isimlerini Türkçeleştirdim ve projeye göre düzenledim.
Mesela VergiHesapla() gibi metodları kendi sistemime uygun şekilde ekledim. Yapay zekadan aldığım mantığı
kullanarak daha iyi yapıya sahip kod yazdım.