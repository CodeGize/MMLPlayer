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
            //����Piano Family:
            {(int)Instrument.AcousticGrandPiano,"���Ǵ����" },
            {(int)Instrument.BrightAcousticPiano,"��ʽ����" },
            {(int)Instrument.ElectricGrandPiano,"�����" },
            {(int)Instrument.HonkyTonkPiano,"�ưɸ���" },
            {(int)Instrument.ElectricPiano1,"��͵����" },
            {(int)Instrument.ElectricPiano2,"���Ǵ����" },
            {(int)Instrument.Harpsichord,"��ܼ���" },
            {(int)Instrument.Clavinet,"����ά������" },

            //�����������Chromatic Percussion Family:
            {(int)Instrument.Celesta,"��Ƭ��" },
            {(int)Instrument.Glockenspiel,"����" },
            {(int)Instrument.MusicBox,"������" },
            {(int)Instrument.Vibraphone,"�����" },
            {(int)Instrument.Marimba,"���ְ�" },
            {(int)Instrument.Xylophone,"ľ��" },
            {(int)Instrument.TubularBells,"����" },
            {(int)Instrument.Dulcimer,"����" },

            //���� Organ Family:
            {(int)Instrument.DrawbarOrgan,"���ɵ·���" },
            {(int)Instrument.PercussiveOrgan,"����ͷ���" },
            {(int)Instrument.RockOrgan,"ҡ������" },
            {(int)Instrument.ChurchOrgan,"�ܷ���" },
            {(int)Instrument.ReedOrgan,"�ɷ���" },
            {(int)Instrument.Accordion,"�ַ���" },
            {(int)Instrument.Harmonica,"����" },
            {(int)Instrument.TangoAccordion,"̽���ַ���" },

            //���� Guitar Family:
            {(int)Instrument.AcousticGuitarNylon,"�����Ҽ���" },
            {(int)Instrument.AcousticGuitarSteel,"���Ҽ���" },
            {(int)Instrument.ElectricGuitarJazz,"��ʿ�ֵ缪��" },
            {(int)Instrument.ElectricGuitarClean,"�����缪��" },
            {(int)Instrument.ElectricGuitarMuted,"�����缪��" },
            {(int)Instrument.OverdrivenGuitar,"������ʧ�棩��Ч�缪��" },
            {(int)Instrument.DistortionGuitar,"ʧ�棨��������Ч�缪��" },
            {(int)Instrument.GuitarHarmonics,"��������" },

            //��˾ Bass Family:
            {(int)Instrument.AcousticBass,"ԭ����˾" },
            {(int)Instrument.ElectricBassFinger,"ָ���籴˾" },
            {(int)Instrument.ElectricBassPick,"��Ƭ���籴˾" },
            {(int)Instrument.FretlessBass,"��Ʒ��˾" },
            {(int)Instrument.SlapBass1,"���ұ�˾2" },
            {(int)Instrument.SlapBass2,"���ұ�˾2" },
            {(int)Instrument.SynthBass1,"�ϳɱ�˾1" },
            {(int)Instrument.SynthBass2,"�ϳɱ�˾2" },

            //���� Strings Family:
            {(int)Instrument.Violin,"С����" },
            {(int)Instrument.Viola,"������" },
            {(int)Instrument.Cello,"������" },
            {(int)Instrument.Contrabass,"��������" },
            {(int)Instrument.TremoloStrings,"��������" },
            {(int)Instrument.PizzicatoStrings,"���ֲ���" },
            {(int)Instrument.OrchestralHarp,"����" },
            {(int)Instrument.Timpani,"������" },

            //�ϳ������ Ensemble Family:
            {(int)Instrument.StringEnsemble1,"���ֺ���1" },
            {(int)Instrument.StringEnsemble2,"���ֺ���2�����ң�" },
            {(int)Instrument.SynthStrings1,"�ϳ�����1" },
            {(int)Instrument.SynthStrings2,"�ϳ�����2�����ң�" },
            {(int)Instrument.ChoirAahs,"�ϳ�����" },
            {(int)Instrument.VoiceOohs,"�������" },
            {(int)Instrument.SynthVoice,"�ϳ�����" },
            {(int)Instrument.OrchestraHit,"�����ֶ���������" },

            //ͭ������ Brass Family:
            {(int)Instrument.Trumpet,"С��" },
            {(int)Instrument.Trombone,"����" },
            {(int)Instrument.Tuba,"���" },
            {(int)Instrument.MutedTrumpet,"����С��" },
            {(int)Instrument.FrenchHorn,"Բ��" },
            {(int)Instrument.BrassSection,"ͭ����" },
            {(int)Instrument.SynthBrass1,"�ϳ�ͭ��1" },
            {(int)Instrument.SynthBrass2,"�ϳ�ͭ��2" },

            //��Ƭ���� Reed Family:
            {(int)Instrument.SopranoSax,"��������˹" },
            {(int)Instrument.AltoSax,"��������˹" },
            {(int)Instrument.TenorSax,"����������˹" },
            {(int)Instrument.BaritoneSax,"�ϵ�������˹" },
            {(int)Instrument.Oboe,"˫�ɹ�" },
            {(int)Instrument.EnglishHorn,"Ӣ����" },
            {(int)Instrument.Bassoon,"���" },
            {(int)Instrument.Clarinet,"���ɹ�" },

            //�������� Pipe Family:
            {(int)Instrument.Piccolo,"�̵�" },
            {(int)Instrument.Flute,"����" },
            {(int)Instrument.Recorder,"����" },
            {(int)Instrument.PanFlute,"�ŵ�" },
            {(int)Instrument.BlownBottle,"��ƿ��" },
            {(int)Instrument.Shakuhachi,"�߰�" },
            {(int)Instrument.Whistle,"��" },
            {(int)Instrument.Ocarina,"�¿�����" },

            //�ϳ����� Synth Lead Family:
            {(int)Instrument.Lead1Square,"�ϳ�����1��������" },
            {(int)Instrument.Lead2Sawtooth,"�ϳ�����2����ݲ���" },
            {(int)Instrument.Lead3Calliope,"�ϳ�����3�����ѷ��٣�" },
            {(int)Instrument.Lead4Chiff,"�ϳ�����4 �����ܣ�" },
            {(int)Instrument.Lead5Charang,"�ϳ�����5��������" },
            {(int)Instrument.Lead6Voice,"�ϳ�����6��������" },
            {(int)Instrument.Lead7Fifths,"�ϳ�����7����ȣ�" },
            {(int)Instrument.Lead8BassPlusLead,"�ϳ�����8����˹��������" },

            // �ϳ�����Synth Pad Family:
            {(int)Instrument.Pad1NewAge,"����ϳ�" },
            {(int)Instrument.Pad2Warm,"��ů�ϳ�" },
            {(int)Instrument.Pad3Polysynth,"�˶ȸ����ϳ�" },
            {(int)Instrument.Pad4Choir,"�ϳ��ϳ�" },
            {(int)Instrument.Pad5Bowed,"����ϳ�" },
            {(int)Instrument.Pad6Metallic,"�����ϳ�" },
            {(int)Instrument.Pad7Halo,"���κϳ�" },
            {(int)Instrument.Pad8Sweep,"��ɨ�ϳ�" },

            //�ϳ�Ч�� Synth Effects Family:
            {(int)Instrument.FX1Rain,"�����ϳ�" },
            {(int)Instrument.FX2Soundtrack,"����ϳ�" },
            {(int)Instrument.FX3Crystal,"ˮ���ϳ�" },
            {(int)Instrument.FX4Atmosphere,"�����ϳ�" },
            {(int)Instrument.FX5Brightness,"�����ϳ�" },
            {(int)Instrument.FX6Goblins,"�粼�ֺϳ�" },
            {(int)Instrument.FX7Echoes,"�����ϳ�" },
            {(int)Instrument.FX8SciFi,"�ƻúϳ�" },

            //�������� Ethnic Family:
            {(int)Instrument.Sitar,"ӡ��������" },
            {(int)Instrument.Banjo,"���ް�׿��" },
            {(int)Instrument.Shamisen,"�ձ�������" },
            {(int)Instrument.Koto,"�ձ�ʮ������" },
            {(int)Instrument.Kalimba,"���޿��ְ�" },
            {(int)Instrument.Bagpipe,"�ո������" },
            {(int)Instrument.Fiddle,"�й�����" },
            {(int)Instrument.Shanai,"����ɽ��" },

            //������� Percussive Family:
            {(int)Instrument.TinkleBell,"������" },
            {(int)Instrument.Agogo,"Agogo��" },
            {(int)Instrument.SteelDrums,"�ֹ�" },
            {(int)Instrument.Woodblock,"ľ��" },
            {(int)Instrument.TaikoDrum,"̫��" },
            {(int)Instrument.MelodicTom,"ͨͨ��" },
            {(int)Instrument.SynthDrum,"�ϳɹ�" },
            {(int)Instrument.ReverseCymbal,"ͭ��" },

            // ��ЧSound Effects Family:
            {(int)Instrument.GuitarFretNoise,"������������" },
            {(int)Instrument.BreathNoise,"������" },
            {(int)Instrument.Seashore,"������" },
            {(int)Instrument.BirdTweet,"����" },
            {(int)Instrument.TelephoneRing,"�绰��" },
            {(int)Instrument.Helicopter,"ֱ����" },
            {(int)Instrument.Applause,"������" },
            {(int)Instrument.Gunshot,"ǹ��" },            
        };
    }
}