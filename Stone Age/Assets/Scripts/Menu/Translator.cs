using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Translator : MonoBehaviour
{
    private static int LanguageId;

    private static List<Translatable_text> listId = new List<Translatable_text>();

    #region ВЕСЬ ТЕКСТ [двухмерный массив]

    private static string[,] LineText =
    {
        #region АНГЛИЙСКИЙ
        {
            "Play",         // 0
            "Continue",     // 1 
            "Levels",       // 2
            "Exit",         // 3
            "Low",          // 4
            "Medium",       // 5
            "Ultra",        // 6
            "Back",         // 7
            "Sound",        // 8
            "Music",        // 9 
            "Sound Effect", // 10
            "Reset",        // 11
            "Record :",                                // 12
            "Menu",                                   // 13
            "Next Level",                            // 14
            "You WIN",                              // 15
            "Defeat",                              // 16
            "Repeat",                             // 17
            "You are drowned, try again!!!",     // 18
            "Yes",                              // 19
            "No",                              // 20
            "Game Over!",                     // 21
            "To win, find a special mushroom, watch the hunger scale, avoid skeletal bites.",         // 22
               "Are you sure you want to quit the game?",                                            // 23
                "If your device has a keyboard connected, use the A, D buttons to move left to right, to hit with the baton use Q, to shoot E, Space jump. If your device supports touch input, use the buttons on the screen.",        // 24
                "You are poisoned, try again from the checkpoint",         //25
                "Hunger took your life, try again from the save point",   //26
                "To pick up the bonus, jump on it and crush",            //27
                "Checkpoint saved"                                      //28
        },
        #endregion

        #region РУССКИЙ
        {
            "Играть",
            "Продолжить",
            "Уровни",
            "Выход",
            "Слабое",
            "Среднее",
            "Ультра",
            "Назад",
            "Звук",
            "Музыка",
            "Звуковые Эффекты",
            "Сброс",
            "Рекорд :",
            "Меню",                                  // 13
            "Следующий уровень",                     // 14
            "ВЫ ВЫИГРАЛИ!!!",                        // 15
            "Поражение!",                            // 16
            "Повторить",                             // 17
            "Вы утонули, попробуйте еще раз !!!",     // 18
                "Да",                                // 19
                "Нет",                               // 20
                "Игра окончена!",                    // 21
                "Чтобы победить найдите особенный гриб, следите за шкалой голода, избегайте укусов скелетов",        // 22
                "Вы действительно хотите выйти из игры?",                                                             // 23
                "Если к вашему девайсу подключенна клавиатура, используйте кнопки A , D для движения влево вправо, для удара дубинкой используйте Q для срельбы E , прыжок Space.Если ваше устройство поддерживает сенсорный ввод - воспользуйтесь кнопками на экране.",         // 24
                "Вы отравились, попробуйте еще раз с контрольной точки",     // 25
                "Голод забрал у вас жизнь, попробуйте снова с точки сохранения",   //26
                "Чтобы забрать бонус прыгните на него и раздавите",   //27
                "Контрольная точка сохранена"         //28
    },
        #endregion

        #region КИТАЙСКИЙ УПРОЩЕННЫЙ
        {
             "玩",                          // 0
             "继续",                        // 1
             "级别",                        // 2
             "输出",                        // 3
             "虚弱的",                      // 4
             "平均",                        // 5
             "极端主义者",                   // 6
             "后退",                        // 7
             "声音",                        // 8
             "音乐",                        // 9
             "声音特效",                    // 10
             "重启",                        // 11
             "记录 :",                      // 12
             "菜单",                        // 13
             "下一级",                      // 14 
             "你赢了",                      // 15
             "打败",                        // 16
             "重复",                        // 17
             "你被淹死了，再试一次",                                         // 18 
             "是的",                                                       // 19
             "不",                                                        // 20
             "游戏结束！",                                                // 21
             "要获胜，请找到特殊的蘑菇，注意饥饿度，避免被骨骼咬伤。",       // 22
             "您确定要退出游戏吗？",                                     // 23
                "如果您的设备连接了键盘，请使用 A D 按钮从左向右移动，使用指挥棒击打使用 Q 射击 E ，空间跳跃 . 如果您的设备支持触摸输入，请使用屏幕上的按钮 .",        // 24
                "你中毒了，从检查站再试一次",            // 25
                "饥饿夺走了你的生命，从保存点再试一次",  //26
                "要获得奖金，请跳上并粉碎",            //27
                "检查点已保存"                       //28
        },
#endregion
    };
    #endregion

    static public void Select_language(int id)
    {
        LanguageId = id;
        Update_texts();

    }

    static public string Get_text(int textKey)
    {
        return LineText[LanguageId, textKey];
    }

    static public void Add(Translatable_text idtext)
    {
        listId.Add(idtext);
    }

    static public void Delete(Translatable_text idtext)
    {
        listId.Remove(idtext);
    }

    static public void Update_texts()
    {
        for(int i = 0; i < listId.Count; i++)
        {
            listId[i].UIText.text = LineText[LanguageId, listId[i].textID];
            if (PlayerPrefs.GetInt("Language") == 1) listId[i].UIText.font = Resources.Load<TMP_FontAsset>("RU_font_asset");
            else if (PlayerPrefs.GetInt("Language") == 2) listId[i].UIText.font = Resources.Load<TMP_FontAsset>("CH_font_asset");
            else listId[i].UIText.font = Resources.Load<TMP_FontAsset>("EN_font_asset");

        }
    }
   
}
