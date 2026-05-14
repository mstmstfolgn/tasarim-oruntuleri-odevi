# Faz 3 - AI Pair Programming Logu

Neler Tartıştık ve Nasıl İlerledik?
Yapay zekayla yaklaşık 1 saatlik bir pair programming yaptık. Asıl amacımız sistemin Açık/Kapalı
Prensibine (OCP) uymasını sağlamaktı. Sepet sınıfında indirimleri hesaplarken aşırı fazla if-else
birikmişti ve kod çorbaya dönmüştü. Bunu temizlemek için oturup **Strategy** pattern'i tartıştık ve koda ekledik.
Ardından sipariş bitince müşteriye SMS ve Email gitsin diye **Observer** pattern'i de aradan çıkardık.

AI olmadan bu faz ne kadar sürerdi?
Açıkçası AI olmasaydı, o if-else bloklarını interface'lere bölmek ve Observer'ın abone sistemini 
sıfırdan kafamda kurup hatasız çalıştırmak rahat 5-6 saatimi alırdı. AI ile eşli çalışarak 1 
saatte işi bitirdik. Tabiki de kopyala yapıştır yapmadım ve kodu anlamaya çalışarak, gerektiğinde müdahale ederek ilerledim.

AI beni nerede yanılttı? (Refleksiyon)
AI ilk kodu verdiğinde mimari bir hata yaptı. Observer'ın bildirim atma kısmını doğrudan `Sepet`
sınıfının içine yazmayı önerdi. Ama Sepet sınıfının tek amacı (Single Responsibility) sepet tutarını hesaplamaktır,
kullanıcıya mesaj atmak onun işi değil. Bunu fark edince koda müdahale ettim ve
bildirim gönderme metodunu (`SiparisTamamlandiBildir`), sipariş sürecini asıl yöneten `ETicaretFacade` 
sınıfına taşıdım. Böylece yapıyı toparlamış oldum.