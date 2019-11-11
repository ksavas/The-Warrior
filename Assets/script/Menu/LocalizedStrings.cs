using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizedStrings  {

    public enum LocalKeys
    {
        SINGLE_SELECTION,
        MULTI_SELECTION,
        OPTIONS_SELECTION,
        EXIT_SELECTION,
        START_GAME,
        JOIN_GAME,
        CREATE_GAME,
        BACK_BUTTON,
        LANGUAGE_LABEL,
        CHAR_DEFINITION_TITLE,
        PLAYER_NAME_LABEL,
        MAP_DEFINITION_COUNT,
        PLAYER_ALIEN,
        PLAYER_SWAT,
        PLAYER_VILLAGER,
        MAP_TITLE,
        PLAYER_COUNT_LABEL,
        EASY_SELECTION,
        MEDIUM_SELECTION,
        HARD_SELECTION,
        DIFFICULTY_LABEL,
        HOST_LIST_NAME,
        HOST_LIST_COUNT,
        HOST_LIST_MAP,
        HOST_LIST_MAP_NAME,
        NAME_ALERT,
        COUNT_ALERT,
        COUNT_RANGER_ALERT_1,
        COUNT_RANGE_ALERT_2,
        GAME_SELECT_ALERT,
        DIFFICULTY_ALERT,
        JOINED_ALERT,
        ENEMY_NAME,
        RIFLE_NAME,
        M9_NAME,
        KNIFE_NAME,
        ALIEN_RIFLE_NAME,
        ALIEN_GUN_NAME,
        LIGHT_SABER_NAME,
        SHOTGUN_NAME,
        REVOLVER_NAME,
        AXE_NAME,
        SCORE_LIST_KILLS,
        SCORE_LIST_DEADS,
        SCORE_LIST_NAME,
        REFRESH_BUTTON,
        ESCAPE_ALERT,
        YES,
        NO,
        WINNER_SHOW
    }

    public class MyStrings
    {
        public LocalKeys key ;
        public string value;
    }
    private static LocalizedStrings localizedStrings;
    public List<MyStrings> localStrings;
    public static LocalizedStrings m_LocalizedStrings
    {
        get
        {
            if (localizedStrings == null)
            {
               localizedStrings = new LocalizedStrings();
               localizedStrings.CreateStrings();
               if (PlayerPrefs.GetString("Language") == "Tr")
                    localizedStrings.CreateTurkishStrings();
               else if (PlayerPrefs.GetString("Language") == "Eng")
                    localizedStrings.CreateEnglishStrings();
               else
                   localizedStrings.CreateTurkishStrings();
                
            }
              
            return localizedStrings;
        }
    }


   public void CreateStrings()
    {
        localStrings = new List<MyStrings>();
        localStrings.Add(new MyStrings { key = LocalKeys.SINGLE_SELECTION, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.MULTI_SELECTION, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.OPTIONS_SELECTION, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.EXIT_SELECTION , value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.START_GAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.JOIN_GAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.CREATE_GAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.LANGUAGE_LABEL, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.BACK_BUTTON, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.CHAR_DEFINITION_TITLE, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.MAP_DEFINITION_COUNT, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.PLAYER_NAME_LABEL, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.PLAYER_ALIEN, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.PLAYER_SWAT, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.PLAYER_VILLAGER, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.MAP_TITLE, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.PLAYER_COUNT_LABEL, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.EASY_SELECTION, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.MEDIUM_SELECTION, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.HARD_SELECTION, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.DIFFICULTY_LABEL, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.HOST_LIST_NAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.HOST_LIST_COUNT, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.HOST_LIST_MAP, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.HOST_LIST_MAP_NAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.NAME_ALERT, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.COUNT_ALERT, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.COUNT_RANGER_ALERT_1, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.COUNT_RANGE_ALERT_2, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.GAME_SELECT_ALERT, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.DIFFICULTY_ALERT, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.JOINED_ALERT, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.ENEMY_NAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.RIFLE_NAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.M9_NAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.KNIFE_NAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.ALIEN_RIFLE_NAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.ALIEN_GUN_NAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.LIGHT_SABER_NAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.SHOTGUN_NAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.REVOLVER_NAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.AXE_NAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.SCORE_LIST_KILLS, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.SCORE_LIST_DEADS, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.SCORE_LIST_NAME, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.REFRESH_BUTTON, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.ESCAPE_ALERT, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.YES, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.NO, value = "" });
        localStrings.Add(new MyStrings { key = LocalKeys.WINNER_SHOW, value = "" });



   }
   public void CreateTurkishStrings()
    {
        localStrings.Find(x => x.key == LocalKeys.SINGLE_SELECTION).value = "Tek Oyuncu" ;
        localStrings.Find(x => x.key == LocalKeys.MULTI_SELECTION).value = "Çok Oyunculu" ;
        localStrings.Find(x => x.key == LocalKeys.OPTIONS_SELECTION).value = "Ayarlar" ;
        localStrings.Find(x => x.key == LocalKeys.EXIT_SELECTION ).value = "Çıkış" ;
        localStrings.Find(x => x.key == LocalKeys.START_GAME).value = "Oyunu Başlat" ;
        localStrings.Find(x => x.key == LocalKeys.JOIN_GAME).value = "Oyuna Katıl" ;
        localStrings.Find(x => x.key == LocalKeys.CREATE_GAME).value = "Oyun Oluştur" ;
        localStrings.Find(x => x.key == LocalKeys.LANGUAGE_LABEL).value = "Dil" ;
        localStrings.Find(x => x.key == LocalKeys.BACK_BUTTON).value = "Geri" ;
        localStrings.Find(x => x.key == LocalKeys.CHAR_DEFINITION_TITLE).value = "Karakter Tanımlama" ;
        localStrings.Find(x => x.key == LocalKeys.MAP_DEFINITION_COUNT).value = "Oyuncu Sayısı" ;
        localStrings.Find(x => x.key == LocalKeys.PLAYER_NAME_LABEL).value = "Oyuncu İsmi" ;
        localStrings.Find(x => x.key == LocalKeys.PLAYER_ALIEN).value = "Uzaylı" ;
        localStrings.Find(x => x.key == LocalKeys.PLAYER_SWAT).value = "Özel Tim" ;
        localStrings.Find(x => x.key == LocalKeys.PLAYER_VILLAGER).value = "Köylü" ;
        localStrings.Find(x => x.key == LocalKeys.MAP_TITLE).value = "Harita Tanımlama" ;
        localStrings.Find(x => x.key == LocalKeys.PLAYER_COUNT_LABEL).value = "Oyuncu Sayısı" ;
        localStrings.Find(x => x.key == LocalKeys.EASY_SELECTION).value = "Kolay" ;
        localStrings.Find(x => x.key == LocalKeys.MEDIUM_SELECTION).value = "Orta" ;
        localStrings.Find(x => x.key == LocalKeys.HARD_SELECTION).value = "Zor" ;
        localStrings.Find(x => x.key == LocalKeys.DIFFICULTY_LABEL).value = "Zorluk : " ;
        localStrings.Find(x => x.key == LocalKeys.HOST_LIST_NAME).value = "İsim" ;
        localStrings.Find(x => x.key == LocalKeys.HOST_LIST_COUNT).value = "Sayı" ;
        localStrings.Find(x => x.key == LocalKeys.HOST_LIST_MAP).value = "Harita" ;
        localStrings.Find(x => x.key == LocalKeys.HOST_LIST_MAP_NAME).value = "Sıradan" ;
        localStrings.Find(x => x.key == LocalKeys.NAME_ALERT).value = "Lütfen İsim Seçiniz" ;
        localStrings.Find(x => x.key == LocalKeys.COUNT_ALERT).value = "Lütfen Oyuncu Sayısı Belirtiniz" ;
        localStrings.Find(x => x.key == LocalKeys.COUNT_RANGER_ALERT_1).value = "Oyuncu Sayısı 2-4" ;
        localStrings.Find(x => x.key == LocalKeys.COUNT_RANGE_ALERT_2).value = "Kişi Arasında Olmalı" ;
        localStrings.Find(x => x.key == LocalKeys.GAME_SELECT_ALERT).value = "Lütfen Oyun Seçiniz" ;
        localStrings.Find(x => x.key == LocalKeys.DIFFICULTY_ALERT).value = "Lütfen Zorluk Seçiniz" ;
        localStrings.Find(x => x.key == LocalKeys.JOINED_ALERT).value = " Bağlandı" ;
        localStrings.Find(x => x.key == LocalKeys.ENEMY_NAME).value = "Düşman" ;
        localStrings.Find(x => x.key == LocalKeys.RIFLE_NAME).value = "Tüfek" ;
        localStrings.Find(x => x.key == LocalKeys.M9_NAME).value = "M9" ;
        localStrings.Find(x => x.key == LocalKeys.KNIFE_NAME).value = "Bıçak" ;
        localStrings.Find(x => x.key == LocalKeys.ALIEN_RIFLE_NAME).value = "Uzaylı Tüfeği" ;
        localStrings.Find(x => x.key == LocalKeys.ALIEN_GUN_NAME).value = "Uzaylı Tabancası" ;
        localStrings.Find(x => x.key == LocalKeys.LIGHT_SABER_NAME).value = "Işın Kılıcı" ;
        localStrings.Find(x => x.key == LocalKeys.SHOTGUN_NAME).value = "Pompalı Tüfek" ;
        localStrings.Find(x => x.key == LocalKeys.REVOLVER_NAME).value = "Altıpatlar" ;
        localStrings.Find(x => x.key == LocalKeys.AXE_NAME).value = "Balta" ;
        localStrings.Find(x => x.key == LocalKeys.SCORE_LIST_KILLS).value = "Öldürme" ;
        localStrings.Find(x => x.key == LocalKeys.SCORE_LIST_DEADS).value = "Ölme" ;
        localStrings.Find(x => x.key == LocalKeys.SCORE_LIST_NAME).value = "İsim" ;
        localStrings.Find(x => x.key == LocalKeys.REFRESH_BUTTON).value = "Yenile" ;
        localStrings.Find(x => x.key == LocalKeys.ESCAPE_ALERT).value = "Çıkmak istediğinizden emin misiniz ?";
        localStrings.Find(x => x.key == LocalKeys.YES).value = "Evet";
        localStrings.Find(x => x.key == LocalKeys.NO).value = "Hayır";
        localStrings.Find(x => x.key == LocalKeys.WINNER_SHOW).value = "Oyun Bitti! Kazanan ";


    }
   public void CreateEnglishStrings()
   {
       localStrings.Find(x => x.key == LocalKeys.SINGLE_SELECTION).value = "";
       localStrings.Find(x => x.key == LocalKeys.SINGLE_SELECTION).value = "Single Player";
       localStrings.Find(x => x.key == LocalKeys.MULTI_SELECTION).value = "Multi Player";
       localStrings.Find(x => x.key == LocalKeys.OPTIONS_SELECTION).value = "Options";
       localStrings.Find(x => x.key == LocalKeys.EXIT_SELECTION).value = "Exit" ;
       localStrings.Find(x => x.key == LocalKeys.START_GAME).value = "Start Game";
       localStrings.Find(x => x.key == LocalKeys.JOIN_GAME).value = "Join a Game";
       localStrings.Find(x => x.key == LocalKeys.CREATE_GAME).value = "Create a Game";
       localStrings.Find(x => x.key == LocalKeys.LANGUAGE_LABEL).value = "Language";
       localStrings.Find(x => x.key == LocalKeys.BACK_BUTTON).value = "Back";
       localStrings.Find(x => x.key == LocalKeys.CHAR_DEFINITION_TITLE).value = "Char Definition";
       localStrings.Find(x => x.key == LocalKeys.MAP_DEFINITION_COUNT).value = "Player Count";
       localStrings.Find(x => x.key == LocalKeys.PLAYER_NAME_LABEL).value = "Player Name";
       localStrings.Find(x => x.key == LocalKeys.PLAYER_ALIEN).value = "Alien";
       localStrings.Find(x => x.key == LocalKeys.PLAYER_SWAT).value = "Swat";
       localStrings.Find(x => x.key == LocalKeys.PLAYER_VILLAGER).value = "Villager";
       localStrings.Find(x => x.key == LocalKeys.MAP_TITLE).value = "Map Definition";
       localStrings.Find(x => x.key == LocalKeys.PLAYER_COUNT_LABEL).value = "Player Count";
       localStrings.Find(x => x.key == LocalKeys.EASY_SELECTION).value = "Easy";
       localStrings.Find(x => x.key == LocalKeys.MEDIUM_SELECTION).value = "Medium";
       localStrings.Find(x => x.key == LocalKeys.HARD_SELECTION).value = "Hard";
       localStrings.Find(x => x.key == LocalKeys.DIFFICULTY_LABEL).value = "Difficulty : ";
       localStrings.Find(x => x.key == LocalKeys.HOST_LIST_NAME).value = "Name";
       localStrings.Find(x => x.key == LocalKeys.HOST_LIST_COUNT).value = "Count";
       localStrings.Find(x => x.key == LocalKeys.HOST_LIST_MAP).value = "Map";
       localStrings.Find(x => x.key == LocalKeys.HOST_LIST_MAP_NAME).value = "Default";
       localStrings.Find(x => x.key == LocalKeys.NAME_ALERT).value = "Plase Define a Name";
       localStrings.Find(x => x.key == LocalKeys.COUNT_ALERT).value = "Please Define a Player Count";
       localStrings.Find(x => x.key == LocalKeys.COUNT_RANGER_ALERT_1).value = "The Player Count Must Be";
       localStrings.Find(x => x.key == LocalKeys.COUNT_RANGE_ALERT_2).value = "Between 2-4";
       localStrings.Find(x => x.key == LocalKeys.GAME_SELECT_ALERT).value = "Please Select a Game";
       localStrings.Find(x => x.key == LocalKeys.DIFFICULTY_ALERT).value = "Please Select a Difficulty";
       localStrings.Find(x => x.key == LocalKeys.JOINED_ALERT).value = " Joined";
       localStrings.Find(x => x.key == LocalKeys.ENEMY_NAME).value = "Enemy";
       localStrings.Find(x => x.key == LocalKeys.RIFLE_NAME).value = "Rifle";
       localStrings.Find(x => x.key == LocalKeys.M9_NAME).value = "M9";
       localStrings.Find(x => x.key == LocalKeys.KNIFE_NAME).value = "Knife";
       localStrings.Find(x => x.key == LocalKeys.ALIEN_RIFLE_NAME).value = "Alien Rifle";
       localStrings.Find(x => x.key == LocalKeys.ALIEN_GUN_NAME).value = "Alien gun";
       localStrings.Find(x => x.key == LocalKeys.LIGHT_SABER_NAME).value = "Light Saber";
       localStrings.Find(x => x.key == LocalKeys.SHOTGUN_NAME).value = "Shotgun";
       localStrings.Find(x => x.key == LocalKeys.REVOLVER_NAME).value = "Revolver";
       localStrings.Find(x => x.key == LocalKeys.AXE_NAME).value = "Axe";
       localStrings.Find(x => x.key == LocalKeys.SCORE_LIST_KILLS).value = "Kills";
       localStrings.Find(x => x.key == LocalKeys.SCORE_LIST_DEADS).value = "Deads";
       localStrings.Find(x => x.key == LocalKeys.SCORE_LIST_NAME).value = "Name";
       localStrings.Find(x => x.key == LocalKeys.REFRESH_BUTTON).value = "Refresh" ;
       localStrings.Find(x => x.key == LocalKeys.ESCAPE_ALERT).value = "Do you really want to exit ?";
       localStrings.Find(x => x.key == LocalKeys.YES).value = "Yes";
       localStrings.Find(x => x.key == LocalKeys.NO).value = "No";
       localStrings.Find(x => x.key == LocalKeys.WINNER_SHOW).value = "The Game is Over! The Winner is ";

   }
    public string GetLocalizedString(LocalKeys key){
        var localizedString = localStrings.Find(x => x.key == key);
        return localizedString.value;
    }
}
