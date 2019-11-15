# The-Warrior

[English](https://github.com/ksavas/The-Warrior/blob/master/README.md)

"The Warrior" hem tek başına hemde çoklu oyuncuyla oynanabilen bilgisayar oyunu. 3. şahıs nişancı görüntüsünde oynanan oyunun amacı, 5 rakibini öldüren ilk kişi olarak oyunu birinci bitirmek. Oyuncu rakiplerini silahlara vurarak öldürür ve oyun alanı can kutuları jet paketler gibi eşylardan oluşur.


## Soyutlama
Bilgisayar mühendisliği bitirme projesi olarak, 3 boyutlu ortamda 3. şahıs nişancı elemanlarının olduğu bir arena savaş oyunu geliştirmeye karar verdik. "The Warrior" oyunu windows işletim sistemi üzerinde oynanan, oyuncuların yapay zekalı botlara karşı veya ağ üzerinden birbirlerine karşı oynadıkları bir seviyede geliştirildi. Oyuncu 3 farklı karakterden birini seçerek oyuna başlayabilir 
(Uzaylı, Özel tim, Köylü). Her karakterin farklı silahları ve her silahında farklı yetenekleri bulunmaktadır. Tek oyunculu seçenekte, oyun yalnızca Özel tim karakterlerine karşı oynanır. Oyun aynı zamanda İngilizce ve Türkçe olarak 2 farklı dil seçeneği sunmaktadır.

## Tek Oyunculu Oynayış
Oyuncu bir karakter seçer, 2 ve 4 arasında rakip oyuncu sayısına karar verir. 3 zorluk seçeneğinden birini seçer ve oyuna başlar.

Oyuncunun kaç kere öldüğünün önemi olmadan, oyuncu 5 öldürme sayısına ulaştığı anda oyun son bulur. Oyun kapandığı esnada hangi oyuncunun kaç kişiyi öldürdüğü ve kaç kere öldüğü bir liste görüntülenir ve oyun kapanır.

## Çoklu oyunculu oynayış
Oyunu açacak kişi host olur ve, "Oyun başlat" seçeneğini seçer, menüden kendi oynayacağı karakteri seçer oyunda oynayacak maksimum oyuncu sayısını belirler(Oyun maksimum oyuncu sayısına ulaşıldığında otomatik olarak başlar)ve oyunu başlatır.

Başlatılan oyuna bağlanacak kişi client olur ve "Oyuna bağlan" seçeneğini seçer, menüden kendi oynayacağı karakteri belirler, host edilen oyunlar listesinden girebileceği oyunlari inceler (girilebilecek oyunlar, harita adı, oyuncu sayısı vs.), istediği oyuna bağlanır ve maksimum sayıya ulaşılınca oyun başlar.

2 oyun modunda da, amaç 5 öldürme sayısına en çabuk ulaşan kişi olmaktır.

Bir oyuncu 5 öldürme sayısına ulaştığında tek oyunculu mod'la aynı süreç işler.

### Oyundan bir görüntü
<img src="https://raw.githubusercontent.com/ksavas/The-Warrior/master/w1.png">

## Menü Tasarımı ve ağ arkaplanının implementasyonu
Menüyü tasarlarken sıradan bir oyun menüsünden çok farklı bir menü tasarımı yapmamaya çalıştık. 2 yönetici script'ten bir tanesi burada çalışmaktadır. Menü'nün bileşenleri:
- Tek Oyuncu
- Çoklu Oyuncu
- Seçenekler
- Çıkış

<img src="https://raw.githubusercontent.com/ksavas/The-Warrior/master/w2.png">

## Tek Oyunculu Menü
Oyuncu Tek oyuncu seçeneğini seçtiği zaman karşısına aşağıdaki gibi bir pencere açılır.:

<img src="https://raw.githubusercontent.com/ksavas/The-Warrior/master/w3.png">

Resimde de görüldüğü gibi; Oyuncu bir karakter seçer, nick adını belirler, rakip oyuncu sayısı ve zorluk derecesini belirleyip oyuna başlar.

GameManager.cs c# dosyası; isim, karakter, oynucu sayısı, zorluk gibi arkaplan bilgilerini kaydeder ve bu bilgileri uygun scriptlere parametre olarak gönderir. "Oyunu başlat" seçeneği seçildiği zaman oyun, oyuncu tarafından yapılan seçimlere göre, başlar.

## Çok Oyunculu Menü
Daha öncede belirttiğimiz gibi, çoklu oyuncunun 2 farklı seçeneği vardır, Oyuncu oyunu başlatan kişi yani host olabilir veya zaten oluşturulmuş bir oyuna client olarak bağlanabilir. Oyuncu çoklu oyuncu seçeneğini seçtiği zaman bu 2 seçenekten birisini seçmek zorundadır. 

Gamemanager.cs, oyuncunun seçeneğine göre uygun scriptleri çalıştırıp oyun akışını başlatır.

# Oyun Oluştur
Kullanıcı "Create a game" seçeneğini seçtiği zaman, tek oyunculu mod'da karşılaştığı gibi bir ekranla karşılaşır. Karakterini seçer, adını belirler, oyuna katılacak oyuncu sayısını belirler, oyunu başlatır ve bağlanacak diğer oyuncuları bekler. Bütün client'lar bağlandığı zaman oyun eşzamanlı olarak başlar.

<img src="https://raw.githubusercontent.com/ksavas/The-Warrior/master/w4.png">

# Oyuna Bağlan
Oyuncu bir oyuna bağlanmak istediği zaman, aşağıdaki gibi bir ekranla karşılaşır:

<img src="https://raw.githubusercontent.com/ksavas/The-Warrior/master/w5.png">

Oyuncu host edilen oyunları görür, bağlanılacak bir oyun seçer, karakterini ve adını belirleyip oynamaya başlar.

## Seçenekler
Oyuncu 2 farklı dil seçeneği arasında seçim yapar.

<img src="https://raw.githubusercontent.com/ksavas/The-Warrior/master/w6.png">

Dile ait bilgileri tutmak için; anahtar,değer çiftlerinin olduğu listelerden kullandık. Enum keyler ve karşılık değerlerini string olarak atadık. Oluşturduğumuz listeyi bir class'ta tuttuk ve oyundaki bütün outputlar string değerlerini bu listeye bağlanarak aldı. Listenin içeriği seçilen dile göre otomatik olarak değişti.
