using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BoxUnlocker;
using BoxUnlocker.Models;
using System.Collections.Generic;
using log4net;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class UnitTestBox
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [TestMethod]
        public void StartUnlockFieldBox()
        {
            logger.Info("■ StartUnlockFieldBox() ==================================================");
            Box box = new Box();
            List<int> validNumbers = new List<int>();

            logger.Info("● フィールド箱4回");
            box.StartUnlockFieldBox(4);
            Assert.AreEqual(0, box.CurrentCount);
            Assert.AreEqual(4, box.MaxCount);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            logger.Info("● フィールド箱5回");
            box.StartUnlockFieldBox(5);
            Assert.AreEqual(0, box.CurrentCount);
            Assert.AreEqual(5, box.MaxCount);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            logger.Info("● フィールド箱6回");
            box.StartUnlockFieldBox(6);
            Assert.AreEqual(0, box.CurrentCount);
            Assert.AreEqual(6, box.MaxCount);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);
        }

        [TestMethod]
        public void StartUnlockMumBox()
        {
            logger.Info("■ StartUnlockMumBox() ==================================================");
            Box box = new Box();
            List<int> validNumbers = new List<int>();

            logger.Info("● MUM箱Gil100");
            box.StartUnlockMumBox(MumType.Gil100);
            Assert.AreEqual(0, box.CurrentCount);
            Assert.AreEqual(6, box.MaxCount);
            validNumbers = new List<int>()
            {
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            logger.Info("● MUM箱Gil200");
            box.StartUnlockMumBox(MumType.Gil200);
            Assert.AreEqual(0, box.CurrentCount);
            Assert.AreEqual(6, box.MaxCount);
            validNumbers = new List<int>()
            {
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            logger.Info("● MUM箱Gil300");
            box.StartUnlockMumBox(MumType.Gil300);
            Assert.AreEqual(0, box.CurrentCount);
            Assert.AreEqual(6, box.MaxCount);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            logger.Info("● MUM箱Gil400");
            box.StartUnlockMumBox(MumType.Gil400);
            Assert.AreEqual(0, box.CurrentCount);
            Assert.AreEqual(5, box.MaxCount);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            logger.Info("● MUM箱Gil500");
            box.StartUnlockMumBox(MumType.Gil500);
            Assert.AreEqual(0, box.CurrentCount);
            Assert.AreEqual(5, box.MaxCount);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            logger.Info("● MUM箱Bayld20");
            box.StartUnlockMumBox(MumType.Bayld20);
            Assert.AreEqual(0, box.CurrentCount);
            Assert.AreEqual(6, box.MaxCount);
            validNumbers = new List<int>()
            {
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            logger.Info("● MUM箱Bayld40");
            box.StartUnlockMumBox(MumType.Bayld40);
            Assert.AreEqual(0, box.CurrentCount);
            Assert.AreEqual(6, box.MaxCount);
            validNumbers = new List<int>()
            {
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            logger.Info("● MUM箱Bayld60");
            box.StartUnlockMumBox(MumType.Bayld60);
            Assert.AreEqual(0, box.CurrentCount);
            Assert.AreEqual(6, box.MaxCount);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            logger.Info("● MUM箱Bayld80");
            box.StartUnlockMumBox(MumType.Bayld80);
            Assert.AreEqual(0, box.CurrentCount);
            Assert.AreEqual(5, box.MaxCount);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);
            
            logger.Info("● MUM箱Bayld100");
            box.StartUnlockMumBox(MumType.Bayld100);
            Assert.AreEqual(0, box.CurrentCount);
            Assert.AreEqual(5, box.MaxCount);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);
        }

        [TestMethod]
        public void GetHintType_IsHint()
        {
            logger.Info("■ GetHintType_IsHint() ==================================================");
            Box box = new Box();
            HintType hintType = HintType.None;
            List<string> regexValue = new List<string>();
            string hint = string.Empty;
            string val1 = string.Empty;
            string val2 = string.Empty;
            string val3 = string.Empty;
            string val4 = string.Empty;

            // ヒントじゃない文字列
            regexValue = new List<string>();
            hint = "ヒントじゃない文字列";
            logger.InfoFormat("●{0}", hint);
            hintType = box.GetHintType(hint, out regexValue);
            Assert.AreEqual(HintType.None, hintType, hint);
            Assert.AreEqual(false, box.IsHint(hint), hint);

            // カギの数字は([0-9]*)より小さいようだ……。
            regexValue = new List<string>();
            for (int i = 10; i <= 99; i++)
            {
                val1 = i.ToString();
                hint = string.Format("カギの数字は{0}より大きいようだ……。", val1);
                logger.InfoFormat("●{0}", hint);
                hintType = box.GetHintType(hint, out regexValue);
                Assert.AreEqual(HintType.Higher, hintType, hint);
                Assert.AreEqual(1, regexValue.Count, hint);
                Assert.AreEqual(val1, regexValue[0], hint);
                Assert.AreEqual(true, box.IsHint(hint), hint);
            }

            // カギの数字は([0-9]*)より大きく、([0-9]*)より小さいようだ……。
            regexValue = new List<string>();
            for (int i = 10; i <= 99; i++)
            {
                val1 = i.ToString();
                val2 = (99 - i).ToString();
                hint = string.Format("カギの数字は{0}より大きく、{1}より小さいようだ……。", val1, val2);
                logger.InfoFormat("●{0}", hint);
                hintType = box.GetHintType(hint, out regexValue);
                Assert.AreEqual(HintType.Between, hintType, hint);
                Assert.AreEqual(2, regexValue.Count, hint);
                Assert.AreEqual(val1, regexValue[0], hint);
                Assert.AreEqual(val2, regexValue[1], hint);
                Assert.AreEqual(true, box.IsHint(hint), hint);
            }

            // カギの数字の([0-9])桁目は([0-9])か([0-9])か([0-9])のどれかのようだ……。
            regexValue = new List<string>();
            for (int i = 1; i <= 2; i++)
            {
                val1 = i.ToString();
                for (int j = 1; j <= 7; j++)
                {
                    val2 = j.ToString();
                    val3 = (j + 1).ToString();
                    val4 = (j + 2).ToString();
                    hint = string.Format("カギの数字の{0}桁目は{1}か{2}か{3}のどれかのようだ……。", val1, val2, val3, val4);
                    logger.InfoFormat("●{0}", hint);
                    hintType = box.GetHintType(hint, out regexValue);
                    Assert.AreEqual(HintType.OneOfThem, hintType, hint);
                    Assert.AreEqual(4, regexValue.Count, hint);
                    Assert.AreEqual(val1, regexValue[0], hint);
                    Assert.AreEqual(val2, regexValue[1], hint);
                    Assert.AreEqual(val3, regexValue[2], hint);
                    Assert.AreEqual(val4, regexValue[3], hint);
                    Assert.AreEqual(true, box.IsHint(hint), hint);
                }
            }

            // カギの2桁の数字のどちらかは([0-9])のようだ……。
            regexValue = new List<string>();
            for (int i = 1; i <= 9; i++)
            {
                val1 = i.ToString();
                hint = string.Format("カギの2桁の数字のどちらかは{0}のようだ……。", val1);
                logger.InfoFormat("●{0}", hint);
                hintType = box.GetHintType(hint, out regexValue);
                Assert.AreEqual(HintType.Either, hintType, hint);
                Assert.AreEqual(1, regexValue.Count, hint);
                Assert.AreEqual(val1, regexValue[0], hint);
                Assert.AreEqual(true, box.IsHint(hint), hint);
            }

            // カギの数字の([0-9])桁目は偶数のようだ……。
            regexValue = new List<string>();
            for (int i = 1; i <= 9; i++)
            {
                val1 = i.ToString();
                hint = string.Format("カギの数字の{0}桁目は偶数のようだ……。", val1);
                logger.InfoFormat("●{0}", hint);
                hintType = box.GetHintType(hint, out regexValue);
                Assert.AreEqual(HintType.Even, hintType, hint);
                Assert.AreEqual(1, regexValue.Count, hint);
                Assert.AreEqual(val1, regexValue[0], hint);
                Assert.AreEqual(true, box.IsHint(hint), hint);
            }

            // カギの数字の([0-9])桁目は奇数のようだ……。
            regexValue = new List<string>();
            for (int i = 1; i <= 9; i++)
            {
                val1 = i.ToString();
                hint = string.Format("カギの数字の{0}桁目は奇数のようだ……。", val1);
                logger.InfoFormat("●{0}", hint);
                hintType = box.GetHintType(hint, out regexValue);
                Assert.AreEqual(HintType.Odd, hintType, hint);
                Assert.AreEqual(1, regexValue.Count, hint);
                Assert.AreEqual(val1, regexValue[0], hint);
                Assert.AreEqual(true, box.IsHint(hint), hint);
            }

            // 何も分からなかった……。
            regexValue = new List<string>();
            hint = "何も分からなかった……。";
            logger.InfoFormat("●{0}", hint);
            hintType = box.GetHintType(hint, out regexValue);
            Assert.AreEqual(HintType.NoHint, hintType, hint);
            Assert.AreEqual(0, regexValue.Count, hint);
            Assert.AreEqual(true, box.IsHint(hint), hint);
            
            // (.*)は、開錠に成功した！
            regexValue = new List<string>();
            val1 = "Player";
            hint = string.Format("{0}は、開錠に成功した！", val1);
            logger.InfoFormat("●{0}", hint);
            hintType = box.GetHintType(hint, out regexValue);
            Assert.AreEqual(HintType.Success, hintType, hint);
            Assert.AreEqual(1, regexValue.Count, hint);
            Assert.AreEqual(val1, regexValue[0], hint);
            Assert.AreEqual(true, box.IsHint(hint), hint);
            
            // (.*)は、開錠に失敗した……。
            regexValue = new List<string>();
            val1 = "Player";
            hint = string.Format("{0}は、開錠に失敗した……。", val1);
            logger.InfoFormat("●{0}", hint);
            hintType = box.GetHintType(hint, out regexValue);
            Assert.AreEqual(HintType.Failed, hintType, hint);
            Assert.AreEqual(1, regexValue.Count, hint);
            Assert.AreEqual(val1, regexValue[0], hint);
            Assert.AreEqual(true, box.IsHint(hint), hint);
        }

        [TestMethod]
        public void AddHintSingle()
        {
            logger.Info("■ AddHintSingle() ==================================================");
            Box box = new Box();
            string hint = string.Empty;
            int maxCount = 0;
            Operation operation = new Operation();
            List<int> validNumbers = new List<int>();

            // ヒントじゃない文字列
            box.StartUnlockFieldBox(6);
            maxCount = 6;
            hint = "ヒントじゃない文字列";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(0, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(0, box.Hints.Count);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);
            Assert.AreEqual(OperationType.ErrorNotHint, operation.OperationType);

            // カギの数字は([0-9]*)より大きいようだ……。
            box.StartUnlockFieldBox(6);
            maxCount = 6;
            hint = "カギの数字は50より大きいようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(1, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(1, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[0]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            // カギの数字は([0-9]*)より小さいようだ……。
            box.StartUnlockFieldBox(6);
            maxCount = 6;
            hint = "カギの数字は50より小さいようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(1, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(1, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[0]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            // カギの数字は([0-9]*)より大きく、([0-9]*)より小さいようだ……。
            box.StartUnlockFieldBox(6);
            maxCount = 6;
            hint = "カギの数字は30より大きく、70より小さいようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(1, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(1, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[0]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            // カギの数字の([0-9])桁目は([0-9])か([0-9])か([0-9])のどれかのようだ……。
            box.StartUnlockFieldBox(6);
            maxCount = 6;
            hint = "カギの数字の1桁目は1か2か3のどれかのようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                11,12,13,
                21,22,23,
                31,32,33,
                41,42,43,
                51,52,53,
                61,62,63,
                71,72,73,
                81,82,83,
                91,92,93,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(1, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(1, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[0]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            //// カギの2桁の数字のどちらかは1のようだ……。
            box.StartUnlockFieldBox(6);
            maxCount = 6;
            hint = "カギの2桁の数字のどちらかは1のようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                   21,
                   31,
                   41,
                   51,
                   61,
                   71,
                   81,
                   91,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(1, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(1, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[0]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            // カギの数字の1桁目は偶数のようだ……。
            box.StartUnlockFieldBox(6);
            maxCount = 6;
            hint = "カギの数字の1桁目は偶数のようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                10,   12,   14,   16,   18,   
                20,   22,   24,   26,   28,   
                30,   32,   34,   36,   38,   
                40,   42,   44,   46,   48,   
                50,   52,   54,   56,   58,   
                60,   62,   64,   66,   68,   
                70,   72,   74,   76,   78,   
                80,   82,   84,   86,   88,   
                90,   92,   94,   96,   98,   
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(1, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(1, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[0]);
            Assert.AreEqual(validNumbers.Count, box.ValidNumbers.Count);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            // カギの数字の2桁目は偶数のようだ……。
            box.StartUnlockFieldBox(6);
            maxCount = 6;
            hint = "カギの数字の2桁目は偶数のようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                20,21,22,23,24,25,26,27,28,29,
                40,41,42,43,44,45,46,47,48,49,
                60,61,62,63,64,65,66,67,68,69,
                80,81,82,83,84,85,86,87,88,89,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(1, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(1, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[0]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            // カギの数字の1桁目は奇数のようだ……。
            box.StartUnlockFieldBox(6);
            maxCount = 6;
            hint = "カギの数字の1桁目は奇数のようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                   11,   13,   15,   17,   19,
                   21,   23,   25,   27,   29,
                   31,   33,   35,   37,   39,
                   41,   43,   45,   47,   49,
                   51,   53,   55,   57,   59,
                   61,   63,   65,   67,   69,
                   71,   73,   75,   77,   79,
                   81,   83,   85,   87,   89,
                   91,   93,   95,   97,   99,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(1, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(1, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[0]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            // カギの数字の2桁目は奇数のようだ……。
            box.StartUnlockFieldBox(6);
            maxCount = 6;
            hint = "カギの数字の2桁目は奇数のようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                30,31,32,33,34,35,36,37,38,39,
                50,51,52,53,54,55,56,57,58,59,
                70,71,72,73,74,75,76,77,78,79,
                90,91,92,93,94,95,96,97,98,99,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(1, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(1, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[0]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            // 何も分からなかった……。
            box.StartUnlockFieldBox(6);
            maxCount = 6;
            hint = "何も分からなかった……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(1, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(1, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[0]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            // Playerは、開錠に成功した！
            box.StartUnlockFieldBox(6);
            maxCount = 6;
            hint = "Playerは、開錠に成功した！";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(1, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(1, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[0]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);
            
            // Playerは、開錠に失敗した……。
            box.StartUnlockFieldBox(6);
            maxCount = 6;
            hint = "Playerは、開錠に失敗した……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(1, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(1, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[0]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);

            /* 雛形
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            */
        }

        [TestMethod]
        public void AddHintMulti1()
        {
            logger.Info("■ AddHintMulti() ==================================================");
            Box box = new Box();
            string hint = string.Empty;
            int maxCount = 0;
            Operation operation = new Operation();
            List<int> validNumbers = new List<int>();

            // カギの数字は([0-9]*)より大きいようだ……。
            // カギの数字は([0-9]*)より小さいようだ……。
            // カギの数字は([0-9]*)より大きく、([0-9]*)より小さいようだ……。
            // カギの数字の([0-9])桁目は([0-9])か([0-9])か([0-9])のどれかのようだ……。
            // カギの2桁の数字のどちらかは([0-9])のようだ……。
            // カギの数字の([0-9])桁目は偶数のようだ……。
            // カギの数字の([0-9])桁目は奇数のようだ……。

            // カギの数字の2桁目は1か2か3のどれかのようだ……。
            maxCount = 6;
            box.StartUnlockFieldBox(maxCount);
            hint = "カギの数字の2桁目は1か2か3のどれかのようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(1, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(1, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[0]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);
            Assert.AreEqual(OperationType.InputNumber, operation.OperationType);
            Assert.AreEqual(24, operation.InputNumber);
            
            // カギの数字は24より大きいようだ……。
            hint = "カギの数字は24より大きいようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                               25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(2, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(2, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[1]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);
            Assert.AreEqual(OperationType.InputNumber, operation.OperationType);
            Assert.AreEqual(32, operation.InputNumber);
            
            // カギの数字は32より小さいようだ……。
            hint = "カギの数字は32より小さいようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                               25,26,27,28,29,
                30,31,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(3, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(3, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[2]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);
            Assert.AreEqual(OperationType.InputNumber, operation.OperationType);
            Assert.AreEqual(28, operation.InputNumber);
            
            // カギの数字は28より小さいようだ……。
            hint = "カギの数字は28より小さいようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                               25,26,27,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(4, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(4, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[3]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);
            Assert.AreEqual(OperationType.InputNumber, operation.OperationType);
            Assert.AreEqual(26, operation.InputNumber);

            // カギの数字は26より大きいようだ……。
            hint = "カギの数字は26より大きいようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                                     27,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(5, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(5, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[4]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);
            Assert.AreEqual(OperationType.InputNumber, operation.OperationType);
            Assert.AreEqual(27, operation.InputNumber);

            /*
            validNumbers = new List<int>()
            {
                10,11,12,13,14,15,16,17,18,19,
                20,21,22,23,24,25,26,27,28,29,
                30,31,32,33,34,35,36,37,38,39,
                40,41,42,43,44,45,46,47,48,49,
                50,51,52,53,54,55,56,57,58,59,
                60,61,62,63,64,65,66,67,68,69,
                70,71,72,73,74,75,76,77,78,79,
                80,81,82,83,84,85,86,87,88,89,
                90,91,92,93,94,95,96,97,98,99,
            };
            */
        }

        [TestMethod]
        public void AddHintMulti2()
        {
            logger.Info("■ AddHintMulti() ==================================================");
            Box box = new Box();
            string hint = string.Empty;
            int maxCount = 0;
            Operation operation = new Operation();
            List<int> validNumbers = new List<int>();

            // カギの数字は([0-9]*)より大きいようだ……。
            // カギの数字は([0-9]*)より小さいようだ……。
            // カギの数字は([0-9]*)より大きく、([0-9]*)より小さいようだ……。
            // カギの数字の([0-9])桁目は([0-9])か([0-9])か([0-9])のどれかのようだ……。
            // カギの2桁の数字のどちらかは([0-9])のようだ……。
            // カギの数字の([0-9])桁目は偶数のようだ……。
            // カギの数字の([0-9])桁目は奇数のようだ……。

            // カギの数字は79より大きいようだ……。
            maxCount = 5;
            box.StartUnlockFieldBox(maxCount);
            hint = "カギの数字は79より大きいようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(1, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(1, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[0]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);
            Assert.AreEqual(OperationType.GetHints, operation.OperationType);
            Assert.AreEqual(0, operation.InputNumber);

            // カギの数字の1桁目は0か1か2のどれかのようだ……。
            maxCount = 5;
            hint = "カギの数字の1桁目は0か1か2のどれかのようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                80,81,82,90,91,92,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(2, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(2, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[1]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);
            Assert.AreEqual(OperationType.InputNumber, operation.OperationType);
            Assert.AreEqual(82, operation.InputNumber);

            // カギの数字は82より大きいようだ……。
            maxCount = 5;
            hint = "カギの数字は82より大きいようだ……。";
            logger.InfoFormat("●{0}", hint);
            validNumbers = new List<int>()
            {
                90,91,92,
            };
            operation = box.AddHint(hint);
            Assert.AreEqual(3, box.CurrentCount);
            Assert.AreEqual(maxCount, box.MaxCount);
            Assert.AreEqual(3, box.Hints.Count);
            Assert.AreEqual(hint, box.Hints[2]);
            CollectionAssert.AreEquivalent(validNumbers, box.ValidNumbers);
            Assert.AreEqual(OperationType.InputNumber, operation.OperationType);
            Assert.AreEqual(91, operation.InputNumber);
        }
    }
}
