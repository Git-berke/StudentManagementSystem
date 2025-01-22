Projenin Özeti
Bu proje, Anakod Yazılım Akademisi tarafından ihtiyaç duyulan öğrenci kayıt, kurs yönetimi ve ödeme takip süreçlerini dijitalleştirmek için geliştirilmiştir. Uygulama, öğrenci ve veli yönetiminden kurs önerilerine kadar birçok özelliği kapsamaktadır. Verimliliği artırmak, hataları minimize etmek ve idari yükleri hafifletmek adına tasarlanmıştır.

Özellikler
1. Kullanıcı Yönetimi
Kullanıcı türleri: Admin, Muhasebe, Standart Kullanıcı.
Yetkilendirme sistemi sayesinde kullanıcılar yalnızca kendi erişim seviyelerine uygun işlemleri gerçekleştirebilir.
Google ile giriş ve "Şifremi Unuttum" özelliği.
2. Öğrenci Yönetimi
Öğrencilerin kayıt, silme ve güncelleme işlemleri.
Kodlama deneyimine göre sınıflandırma.
Öğrencilerin bilgilerini içeren veri aktarımı (Excel’e Export/Import).
3. Veli Yönetimi
Veliler için ayrı bir yönetim modülü.
Öğrenci ile ilişkilendirilmiş veli bilgileri.
Mail bildirim sistemi.
4. Kurs Yönetimi
Kurs oluşturma, düzenleme ve silme.
Kurslara öğrenci ve eğitmen atama.
Kurs seviyelendirme: Başlangıç, Orta, İleri düzey kurslar (ör. Python Level 1, 2, 3).
5. Ödeme Yönetimi
Ödeme durumu takip (ödenmiş, ödenmemiş, takip edilecek).
Taksitlendirme ve ödenen tutarların izlenmesi.
Ödeme makbuzlarını PDF formatında saklama.
6. Kurs Öneri Sistemi
Bitirilen kurslara göre öğrencilere uygun kurslar önerilir.
Öneriler öğrenci geçmişiyle ilişkilendirilerek e-posta ile paylaşılabilir.
7. Mail Entegrasyonu
Öğrenciler ve veliler için toplu ya da bireysel bilgilendirme e-postaları.
Gönderilen e-postaların veritabanında saklanması.
8. Excel Entegrasyonu
Veritabanındaki tüm bilgileri Excel formatında dışa aktarma.
Excel dosyalarından toplu veri yükleme.
Kullanılan Teknolojiler
Arayüz: Windows Forms (C#)
Veritabanı: Microsoft SQL Server
E-posta Gönderimi: SMTP (Simple Mail Transfer Protocol)
Entegrasyonlar: Google API (OAuth Login), ClosedXML (Excel işlemleri)
Sorgular: T-SQL (Stored Procedures ve Trigger’lar)
Veri Yapısı: Entity-Relationship (ER) Diyagramına göre tasarlandı.
Veritabanı Yapısı
Projede kullanılan başlıca tablolar:

Students: Öğrenci bilgileri.
Guardians: Velilerin iletişim bilgileri.
Courses: Kurs bilgileri (isim, seviye, tip, kapasite).
Payments: Ödeme durumları ve taksitlendirme.
InstructorAssignments: Eğitmen-kurs ilişkileri.
Suggestions: Kurs önerileri.
Receipts: Ödeme makbuzları.
Örnek Sorgular
Birden fazla kursa kayıtlı öğrencileri listeleme.
Her eğitmenin yönettiği kurs ve öğrenci sayısını gösterme.
Öğrenci yaş gruplarına göre dağılım.
Kullanıcı Arayüzü
Giriş Ekranı: Google ile giriş ve şifre sıfırlama.
Ana Sayfa: Genel istatistikler ve hızlı erişim düğmeleri.
Öğrenci Paneli: Öğrenci yönetimi ve Excel entegrasyonu.
Ödeme Paneli: Ödeme durumu takibi ve makbuz yönetimi.
Kurs Paneli: Kurs ve seviye yönetimi.
Eğitmen Paneli: Eğitmen bilgilerini düzenleme.
Bilgilendirme Paneli: Mail gönderim araçları.
