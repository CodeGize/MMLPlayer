using System;
using System.Collections.Generic;
using Midi;
using UnityEngine;
namespace MMLPlayer
{
    public class InstrumentSelectWnd : MonoBehaviour
    {
        public GameObject ItemPrefab;

        void Start()
        {
            var instruments = Enum.GetValues(typeof(Instrument));
            foreach (var instrument in instruments)
            {
                var itemgo = Instantiate(ItemPrefab, ItemPrefab.transform.parent);
                itemgo.SetActive(true);
                var btn = itemgo.GetComponent<InstrumentBtn>();
                btn.Init((int)instrument);
            }
        }


        public void OnItemClick(InstrumentBtn go)
        {
            gameObject.SetActive(false);
            m_callback?.Invoke(go.Instrument);
        }

        private Action<int> m_callback;
        internal void Show(Action<int> callback)
        {
            m_callback = callback;
            gameObject.SetActive(true);
        }

        public static string GetInstrumentName(int type)
        {
            return InstrumentNames[type];
        }

        private static Dictionary<int, string> InstrumentNames = new Dictionary<int, string>
        {
            //钢琴Piano Family:
            {(int)Instrument.AcousticGrandPiano,"三角大钢琴" },
            {(int)Instrument.BrightAcousticPiano,"立式钢琴" },
            {(int)Instrument.ElectricGrandPiano,"电钢琴" },
            {(int)Instrument.HonkyTonkPiano,"酒吧钢琴" },
            {(int)Instrument.ElectricPiano1,"柔和电钢琴" },
            {(int)Instrument.ElectricPiano2,"三角打钢琴" },
            {(int)Instrument.Harpsichord,"羽管键琴" },
            {(int)Instrument.Clavinet,"科拉维科特琴" },

            //半音打击乐器Chromatic Percussion Family:
            {(int)Instrument.Celesta,"钢片琴" },
            {(int)Instrument.Glockenspiel,"钟琴" },
            {(int)Instrument.MusicBox,"八音盒" },
            {(int)Instrument.Vibraphone,"电颤琴" },
            {(int)Instrument.Marimba,"马林巴" },
            {(int)Instrument.Xylophone,"木琴" },
            {(int)Instrument.TubularBells,"管钟" },
            {(int)Instrument.Dulcimer,"扬琴" },

            //风琴 Organ Family:
            {(int)Instrument.DrawbarOrgan,"哈蒙德风琴" },
            {(int)Instrument.PercussiveOrgan,"打击型风琴" },
            {(int)Instrument.RockOrgan,"摇滚风琴" },
            {(int)Instrument.ChurchOrgan,"管风琴" },
            {(int)Instrument.ReedOrgan,"簧风琴" },
            {(int)Instrument.Accordion,"手风琴" },
            {(int)Instrument.Harmonica,"口琴" },
            {(int)Instrument.TangoAccordion,"探戈手风琴" },

            //吉他 Guitar Family:
            {(int)Instrument.AcousticGuitarNylon,"尼龙弦吉他" },
            {(int)Instrument.AcousticGuitarSteel,"钢弦吉他" },
            {(int)Instrument.ElectricGuitarJazz,"爵士乐电吉他" },
            {(int)Instrument.ElectricGuitarClean,"清音电吉他" },
            {(int)Instrument.ElectricGuitarMuted,"弱音电吉他" },
            {(int)Instrument.OverdrivenGuitar,"驱动（失真）音效电吉他" },
            {(int)Instrument.DistortionGuitar,"失真（破音）音效电吉他" },
            {(int)Instrument.GuitarHarmonics,"吉他泛音" },

            //贝司 Bass Family:
            {(int)Instrument.AcousticBass,"原声贝司" },
            {(int)Instrument.ElectricBassFinger,"指拨电贝司" },
            {(int)Instrument.ElectricBassPick,"拨片拨电贝司" },
            {(int)Instrument.FretlessBass,"无品贝司" },
            {(int)Instrument.SlapBass1,"击弦贝司2" },
            {(int)Instrument.SlapBass2,"击弦贝司2" },
            {(int)Instrument.SynthBass1,"合成贝司1" },
            {(int)Instrument.SynthBass2,"合成贝司2" },

            //弦乐 Strings Family:
            {(int)Instrument.Violin,"小提琴" },
            {(int)Instrument.Viola,"中提琴" },
            {(int)Instrument.Cello,"大提琴" },
            {(int)Instrument.Contrabass,"低音提琴" },
            {(int)Instrument.TremoloStrings,"弦乐震音" },
            {(int)Instrument.PizzicatoStrings,"弦乐拨奏" },
            {(int)Instrument.OrchestralHarp,"竖琴" },
            {(int)Instrument.Timpani,"定音鼓" },

            //合唱或合奏 Ensemble Family:
            {(int)Instrument.StringEnsemble1,"弦乐合奏1" },
            {(int)Instrument.StringEnsemble2,"弦乐合奏2（柔弦）" },
            {(int)Instrument.SynthStrings1,"合成弦乐1" },
            {(int)Instrument.SynthStrings2,"合成弦乐2（柔弦）" },
            {(int)Instrument.ChoirAahs,"合唱啊音" },
            {(int)Instrument.VoiceOohs,"人声嘟音" },
            {(int)Instrument.SynthVoice,"合成人声" },
            {(int)Instrument.OrchestraHit,"管弦乐队重音齐奏" },

            //铜管乐器 Brass Family:
            {(int)Instrument.Trumpet,"小号" },
            {(int)Instrument.Trombone,"长号" },
            {(int)Instrument.Tuba,"大号" },
            {(int)Instrument.MutedTrumpet,"弱音小号" },
            {(int)Instrument.FrenchHorn,"圆号" },
            {(int)Instrument.BrassSection,"铜管组" },
            {(int)Instrument.SynthBrass1,"合成铜管1" },
            {(int)Instrument.SynthBrass2,"合成铜管2" },

            //哨片乐器 Reed Family:
            {(int)Instrument.SopranoSax,"高音萨克斯" },
            {(int)Instrument.AltoSax,"中音萨克斯" },
            {(int)Instrument.TenorSax,"次中音萨克斯" },
            {(int)Instrument.BaritoneSax,"上低音萨克斯" },
            {(int)Instrument.Oboe,"双簧管" },
            {(int)Instrument.EnglishHorn,"英国管" },
            {(int)Instrument.Bassoon,"大管" },
            {(int)Instrument.Clarinet,"单簧管" },

            //吹管乐器 Pipe Family:
            {(int)Instrument.Piccolo,"短笛" },
            {(int)Instrument.Flute,"长笛" },
            {(int)Instrument.Recorder,"竖笛" },
            {(int)Instrument.PanFlute,"排笛" },
            {(int)Instrument.BlownBottle,"吹瓶口" },
            {(int)Instrument.Shakuhachi,"尺八" },
            {(int)Instrument.Whistle,"哨" },
            {(int)Instrument.Ocarina,"奥卡雷那" },

            //合成主音 Synth Lead Family:
            {(int)Instrument.Lead1Square,"合成主音1（方波）" },
            {(int)Instrument.Lead2Sawtooth,"合成主音2（锯齿波）" },
            {(int)Instrument.Lead3Calliope,"合成主音3（汽笛风琴）" },
            {(int)Instrument.Lead4Chiff,"合成主音4 （吹管）" },
            {(int)Instrument.Lead5Charang,"合成主音5（吉他）" },
            {(int)Instrument.Lead6Voice,"合成主音6（人声）" },
            {(int)Instrument.Lead7Fifths,"合成主音7（五度）" },
            {(int)Instrument.Lead8BassPlusLead,"合成主音8（贝斯加主音）" },

            // 合成柔音Synth Pad Family:
            {(int)Instrument.Pad1NewAge,"幻想合成" },
            {(int)Instrument.Pad2Warm,"温暖合成" },
            {(int)Instrument.Pad3Polysynth,"八度复音合成" },
            {(int)Instrument.Pad4Choir,"合唱合成" },
            {(int)Instrument.Pad5Bowed,"弓奏合成" },
            {(int)Instrument.Pad6Metallic,"金属合成" },
            {(int)Instrument.Pad7Halo,"光晕合成" },
            {(int)Instrument.Pad8Sweep,"横扫合成" },

            //合成效果 Synth Effects Family:
            {(int)Instrument.FX1Rain,"雨声合成" },
            {(int)Instrument.FX2Soundtrack,"音轨合成" },
            {(int)Instrument.FX3Crystal,"水晶合成" },
            {(int)Instrument.FX4Atmosphere,"大气合成" },
            {(int)Instrument.FX5Brightness,"明亮合成" },
            {(int)Instrument.FX6Goblins,"哥布林合成" },
            {(int)Instrument.FX7Echoes,"回声合成" },
            {(int)Instrument.FX8SciFi,"科幻合成" },

            //民族乐器 Ethnic Family:
            {(int)Instrument.Sitar,"印度西塔尔" },
            {(int)Instrument.Banjo,"美洲班卓琴" },
            {(int)Instrument.Shamisen,"日本三昧线" },
            {(int)Instrument.Koto,"日本十三弦筝" },
            {(int)Instrument.Kalimba,"非洲卡林巴" },
            {(int)Instrument.Bagpipe,"苏格兰风笛" },
            {(int)Instrument.Fiddle,"中国提琴" },
            {(int)Instrument.Shanai,"澳洲山奈" },

            //打击乐器 Percussive Family:
            {(int)Instrument.TinkleBell,"叮当铃" },
            {(int)Instrument.Agogo,"Agogo铃" },
            {(int)Instrument.SteelDrums,"钢鼓" },
            {(int)Instrument.Woodblock,"木鱼" },
            {(int)Instrument.TaikoDrum,"太鼓" },
            {(int)Instrument.MelodicTom,"通通鼓" },
            {(int)Instrument.SynthDrum,"合成鼓" },
            {(int)Instrument.ReverseCymbal,"铜钹" },

            // 声效Sound Effects Family:
            {(int)Instrument.GuitarFretNoise,"吉他换把杂声" },
            {(int)Instrument.BreathNoise,"呼吸声" },
            {(int)Instrument.Seashore,"海浪声" },
            {(int)Instrument.BirdTweet,"鸟鸣" },
            {(int)Instrument.TelephoneRing,"电话铃" },
            {(int)Instrument.Helicopter,"直升机" },
            {(int)Instrument.Applause,"鼓掌声" },
            {(int)Instrument.Gunshot,"枪声" },            
        };
    }
}