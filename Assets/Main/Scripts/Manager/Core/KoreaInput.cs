using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class KoreaInput : MonoBehaviour
{
    private const int KEY_CODE_SPACE = -1;        // 띄어쓰기
    private const int KEY_CODE_ENTER = -2;      // 내려쓰기
    private const int KEY_CODE_BACKSPACE = -3;      // 지우기
    private const int BASE_CODE = 0xac00;       // 기초음성(가)
    private const int LIMIT_MIN = 0xac00;       // 음성범위 MIN(가)
    private const int LIMIT_MAX = 0xd7a3;       // 음성범위 MAX

    private enum HAN_STATUS
    {
        HS_FIRST = 0, // 초성
        HS_FIRST_V,         // 자음 + 자음 
        HS_FIRST_C,         // 모음 + 모음
        HS_MIDDLE_STATE,    // 초성 + 모음 + 모음
        HS_END,             // 초성 + 중성 + 종성
        HS_END_STATE,       // 초성 + 중성 + 자음 + 자음
        HS_END_EXCEPTION    // 초성 + 중성 + 종성(곁자음)
    };

    //public char ingWord;
    public char ingWord;
    public string completeText;

    // 음성 테이블
    char[] SOUND_TABLE = new char[68]
    {
            'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ',
             'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ',
            'ㅆ', 'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ',
            'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ',
	        /* 중성 21자 19 ~ 39 */
	        'ㅏ', 'ㅐ', 'ㅑ', 'ㅒ', 'ㅓ',
            'ㅔ', 'ㅕ', 'ㅖ', 'ㅗ', 'ㅘ',
            'ㅙ', 'ㅚ', 'ㅛ', 'ㅜ', 'ㅝ',
            'ㅞ', 'ㅟ', 'ㅠ', 'ㅡ', 'ㅢ',
            'ㅣ',
	        /* 종성 28자 40 ~ 67 */
	        ' ', 'ㄱ', 'ㄲ', 'ㄳ', 'ㄴ',
            'ㄵ', 'ㄶ', 'ㄷ', 'ㄹ', 'ㄺ',
            'ㄻ', 'ㄼ', 'ㄽ', 'ㄾ', 'ㄿ',
            'ㅀ', 'ㅁ', 'ㅂ', 'ㅄ', 'ㅅ',
            'ㅆ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ',
            'ㅌ', 'ㅍ', 'ㅎ'
    };
    // 초성 합성 테이블
    int[,] MIXED_CHO_CONSON = new int[14, 3]
    {
            { 0, 0,15 }, // ㄱ,ㄱ,ㅋ
	        {15, 0, 1}, // ㅋ,ㄱ,ㄲ
	        { 1, 0, 0}, // ㄲ,ㄱ,ㄱ

	        { 3, 3,16}, // ㄷ,ㄷ,ㅌ
	        {16, 3, 4}, // ㅌ,ㄷ,ㄸ
	        { 4, 3, 3}, // ㄸ,ㄷ,ㄷ

	        { 7, 7,17}, // ㅂ,ㅂ,ㅍ
	        {17, 7, 8}, // ㅍ,ㅂ,ㅃ
	        { 8, 7, 7}, // ㅃ,ㅂ,ㅂ

            { 9, 9,10}, // ㅅ,ㅅ,ㅆ
	        {10, 9, 9}, // ㅆ,ㅅ,ㅅ

	        {12,12,14}, // ㅈ,ㅈ,ㅊ
	        {14,12,13}, // ㅊ,ㅈ,ㅉ
	        {13,12,12}  // ㅉ,ㅈ,ㅈ
    };


    // 초성,중성 모음 합성 테이블
    int[,] MIXED_VOWEL = new int[21, 3]
    {
            {19,19,21},	// ㅏ,ㅏ,ㅑ
	        {21,19,19},	// ㅑ,ㅏ,ㅏ

	        {19,39,20},	// ㅏ,ㅣ,ㅐ
	        {21,39,22},	// ㅑ,ㅣ,ㅒ

	        {23,23,25},	// ㅓ,ㅓ,ㅕ
	        {25,23,23},	// ㅕ,ㅓ,ㅓ

	        {23,39,24},	// ㅓ,ㅣ,ㅔ
	        {25,39,26},	// ㅕ,ㅣ,ㅖ

	        {27,27,31},	// ㅗ,ㅗ,ㅛ
	        {31,27,27},	// ㅛ,ㅗ,ㅗ

	        {27,19,28},	// ㅗ,ㅏ,ㅘ
	        {28,39,29},	// ㅘ,ㅣ,ㅙ

	        {27,39,30},	// ㅗ,ㅣ,ㅚ

	        {32,32,36},	// ㅜ,ㅜ,ㅠ
	        {36,32,32},	// ㅠ,ㅜ,ㅜ

	        {32,23,33},	// ㅜ,ㅓ,ㅝ
	        {33,39,34},	// ㅝ,ㅣ,ㅞ

	        {32,39,35},	// ㅜ,ㅣ,ㅟ

	        {39,39,37},	// ㅣ,ㅣ,ㅡ
	        {37,39,38},	// ㅡ,ㅣ,ㅢ
	        {38,39,39}  // ㅢ,ㅣ,ㅣ
    };

    // 종성 합성 테이블
    int[,] MIXED_JONG_CONSON = new int[22, 3]
    {
            {41,41,64}, // ㄱ,ㄱ,ㅋ
	        {64,41,42}, // ㅋ,ㄱ,ㄲ
	        {42,41,41}, // ㄲ,ㄱ,ㄱ

	        {41,59,43}, // ㄱ,ㅅ,ㄳ

	        {44,62,45}, // ㄴ,ㅈ,ㄵ
	        {44,67,46}, // ㄴ,ㅎ,ㄶ

	        {47,47,65}, // ㄷ,ㄷ,ㅌ
	        {65,47,47}, // ㅌ,ㄷ,ㄷ

	        {48,41,49}, // ㄹ,ㄱ,ㄺ
	        {48,56,50}, // ㄹ,ㅁ,ㄻ

	        {48,57,51}, // ㄹ,ㅂ,ㄼ
	        {51,57,54}, // ㄼ,ㅂ,ㄿ

	        {48,59,52}, // ㄹ,ㅅ,ㄽ
	        {48,47,53}, // ㄹ,ㄷ,ㄾ	
	        {48,67,55}, // ㄹ,ㅎ,ㅀ

	        {57,57,66}, // ㅂ,ㅂ,ㅍ
	        {66,57,57}, // ㅍ,ㅂ,ㅂ

	        {57,59,58}, // ㅂ,ㅅ,ㅄ

	        {59,59,60}, // ㅅ,ㅅ,ㅆ
	        {60,59,59}, // ㅆ,ㅅ,ㅅ

	        {62,62,63}, // ㅈ,ㅈ,ㅊ
	        {63,62,62}  // ㅊ,ㅈ,ㅈ
    };

    // 종성 분해 테이블
    int[,] DIVIDE_JONG_CONSON = new int[13, 3]
    {
            {41,41,42}, // ㄱ,ㄱ,ㄲ
	        {41,59,43}, // ㄱ,ㅅ,ㄳ
	        {44,62,45}, // ㄴ,ㅈ,ㄵ
	        {44,67,46}, // ㄴ,ㅎ,ㄶ
	        {48,41,49}, // ㄹ,ㄱ,ㄺ
	        {48,56,50}, // ㄹ,ㅁ,ㄻ
	        {48,57,51}, // ㄹ,ㅂ,ㄼ
	        {48,66,54}, // ㄹ,ㅍ,ㄿ
	        {48,59,52}, // ㄹ,ㅅ,ㄽ
	        {48,65,53}, // ㄹ,ㅌ,ㄾ	
	        {48,67,55}, // ㄹ,ㅎ,ㅀ
	        {57,59,58}, // ㅂ,ㅅ,ㅄ
	        {59,59,60}  // ㅅ,ㅅ,ㅆ
    };


    private int m_nStatus;      // 단어조합상태
    int[] m_nPhonemez = new int[5]; // 음소[초,중,종,곁자음1,곁자음2]

    private char m_completeWord;    // 완성글자


    public KoreaInput() // 생성자
    {
        Clear();
    }


    public Text nickText;


    public void FixedUpdate()
    {
        nickText.text = completeText + ingWord;
    }

    public void Clear() // 버퍼 초기화
    {
        m_nStatus = (int)HAN_STATUS.HS_FIRST;
        completeText = ("");
        ingWord = '\0';
        m_completeWord = '\0';
    }

    // ***********포인터 이슈있음**************
    public char SetKeyCode(int nKeyCode)
    {
        m_completeWord = '\0';
        if (nKeyCode < 0) // 특수키 입력..
        {
            m_nStatus = (int)HAN_STATUS.HS_FIRST;
            if (nKeyCode == KEY_CODE_SPACE) // 스페이스바
            {
                if (ingWord != '\0')
                {
                    completeText += ingWord;
                }
                else
                {
                    completeText += ' ';
                }
                ingWord = '\0'; // null 입력
            }
            else if (nKeyCode == KEY_CODE_ENTER) // 내려쓰기
            {
                if (ingWord != '\0')
                {
                    completeText += ingWord;
                }
                completeText += "\r\n";
                ingWord = '\0';
            }
            else if (nKeyCode == KEY_CODE_BACKSPACE) // 지우기
            {
                if (ingWord == '\0')
                {
                    if (completeText.Length > 0)
                    {
                        // 
                        if (completeText.Substring(completeText.Length - 1) == "\n") // 맨끝문자가 널인지
                        {
                            completeText = completeText.Substring(0, completeText.Length - 2);
                        }
                        else
                        {
                            completeText = completeText.Substring(0, completeText.Length - 1);
                        }

                    }
                }
                else
                {
                    m_nStatus = DownGradeIngWordStatus(ingWord);
                }
            }

            return m_completeWord;
        }

        switch (m_nStatus)
        {
            case (int)HAN_STATUS.HS_FIRST:
                // 초성
                m_nPhonemez[0] = nKeyCode;
                ingWord = SOUND_TABLE[m_nPhonemez[0]];
                m_nStatus = nKeyCode > 18 ? (int)HAN_STATUS.HS_FIRST_C : (int)HAN_STATUS.HS_FIRST_V;
                break;
            case (int)HAN_STATUS.HS_FIRST_C:
                // 모음 + 모음
                if (nKeyCode > 18) // 모음
                {
                    unsafe
                    {
                        int* data = PointerFunction.ToPointer(m_nPhonemez);
                        //if (MixVowel(&m_nPhonemez[0], nKeyCode) == false)
                        if (MixVowel(data, nKeyCode) == false)
                        {
                            m_completeWord = SOUND_TABLE[m_nPhonemez[0]];
                            m_nPhonemez[0] = nKeyCode;
                        }
                    }
                }
                else // 자음
                {
                    m_completeWord = SOUND_TABLE[m_nPhonemez[0]];
                    m_nPhonemez[0] = nKeyCode;
                    m_nStatus = (int)HAN_STATUS.HS_FIRST_V;
                }
                break;

            case (int)HAN_STATUS.HS_FIRST_V:
                // 자음 + 자음
                if (nKeyCode > 18) // 모음
                {
                    m_nPhonemez[1] = nKeyCode;
                    m_nStatus = (int)HAN_STATUS.HS_MIDDLE_STATE;
                }
                else
                {
                    if (!MixInitial(nKeyCode))
                    {
                        m_completeWord = SOUND_TABLE[m_nPhonemez[0]];
                        m_nPhonemez[0] = nKeyCode;
                        m_nStatus = (int)HAN_STATUS.HS_FIRST_V;
                    }
                }
                break;

            case (int)HAN_STATUS.HS_MIDDLE_STATE:
                // 초성 + 모음 + 모음
                if (nKeyCode > 18)
                {
                    unsafe
                    {
                        int* data = PointerFunction.ToPointer(m_nPhonemez);
                        //if (MixVowel(&m_nPhonemez[1], nKeyCode) == false)
                        if (MixVowel(&data[1], nKeyCode) == false)
                        {
                            m_completeWord = CombineHangle(1);
                            m_nPhonemez[0] = nKeyCode;
                            m_nStatus = (int)HAN_STATUS.HS_FIRST_C;
                        }
                    }

                }
                else
                {
                    int jungCode = ToFinal(nKeyCode);
                    if (jungCode > 0)
                    {
                        m_nPhonemez[2] = jungCode;
                        m_nStatus = (int)HAN_STATUS.HS_END_STATE;
                    }
                    else
                    {
                        m_completeWord = CombineHangle(1);
                        m_nPhonemez[0] = nKeyCode;
                        m_nStatus = (int)HAN_STATUS.HS_FIRST;
                    }
                }
                break;
            case (int)HAN_STATUS.HS_END:
                // 초성 + 중성 + 종성
                if (nKeyCode > 18)
                {
                    m_completeWord = CombineHangle(1);
                    m_nPhonemez[0] = nKeyCode;
                    m_nStatus = (int)HAN_STATUS.HS_FIRST;
                }
                else
                {
                    int jungCode = ToFinal(nKeyCode);
                    if (jungCode > 0)
                    {
                        m_nPhonemez[2] = jungCode;
                        m_nStatus = (int)HAN_STATUS.HS_END_STATE;
                    }
                    else
                    {
                        m_completeWord = CombineHangle(1);
                        m_nPhonemez[0] = nKeyCode;
                        m_nStatus = (int)HAN_STATUS.HS_FIRST;
                    }
                }
                break;

            case (int)HAN_STATUS.HS_END_STATE:
                // 초성 + 중성 + 자음(종) + 자음(종)
                if (nKeyCode > 18)
                {
                    m_completeWord = CombineHangle(1);

                    m_nPhonemez[0] = ToInitial(m_nPhonemez[2]);
                    m_nPhonemez[1] = nKeyCode;
                    m_nStatus = (int)HAN_STATUS.HS_MIDDLE_STATE;
                }
                else
                {
                    int jungCode = ToFinal(nKeyCode);
                    if (jungCode > 0)
                    {
                        m_nStatus = (int)HAN_STATUS.HS_END_EXCEPTION;

                        if (!MixFinal(jungCode))
                        {
                            m_completeWord = CombineHangle(2);
                            m_nPhonemez[0] = nKeyCode;
                            m_nStatus = (int)HAN_STATUS.HS_FIRST_V;
                        }
                    }
                    else
                    {
                        m_completeWord = CombineHangle(2);
                        m_nPhonemez[0] = nKeyCode;
                        m_nStatus = (int)HAN_STATUS.HS_FIRST_V;
                    }
                }
                break;

            case (int)HAN_STATUS.HS_END_EXCEPTION:
                // 초성 + 중성 + 종성(곁자음)
                if (nKeyCode > 18)
                {
                    DecomposeConsonant();
                    m_nPhonemez[1] = nKeyCode;
                    m_nStatus = (int)HAN_STATUS.HS_MIDDLE_STATE;
                }
                else
                {
                    int jungCode = ToFinal(nKeyCode);
                    if (jungCode > 0)
                    {
                        m_nStatus = (int)HAN_STATUS.HS_END_EXCEPTION;

                        if (!MixFinal(jungCode))
                        {
                            m_completeWord = CombineHangle(2);
                            m_nPhonemez[0] = nKeyCode;
                            m_nStatus = (int)HAN_STATUS.HS_FIRST_V;
                        }
                    }
                    else
                    {
                        m_completeWord = CombineHangle(2);
                        m_nPhonemez[0] = nKeyCode;
                        m_nStatus = (int)HAN_STATUS.HS_FIRST_V;
                    }
                }
                break;
        }
        CombineIngWord(m_nStatus);
        if (m_completeWord != '\0')
            completeText += m_completeWord;
        return m_completeWord;
    }

    // 변환 
    private int ToInitial(int nKeyCode) // 초성으로
    {
        switch (nKeyCode)
        {
            case 41: return 0;  // ㄱ
            case 42: return 1;  // ㄲ
            case 44: return 2;  // ㄴ
            case 47: return 3;  // ㄷ
            case 48: return 5;  // ㄹ
            case 56: return 6;  // ㅁ
            case 57: return 7;  // ㅂ
            case 59: return 9;  // ㅅ
            case 60: return 10; // ㅆ
            case 61: return 11; // ㅇ
            case 62: return 12; // ㅈ
            case 63: return 14; // ㅊ
            case 64: return 15; // ㅋ
            case 65: return 16; // ㅌ
            case 66: return 17; // ㅍ
            case 67: return 18; // ㅎ
        }
        return -1;
    }
    private int ToFinal(int nKeyCode)       // 종성으로
    {
        switch (nKeyCode)
        {
            case 0: return 41;  // ㄱ
            case 1: return 42;  // ㄲ
            case 2: return 44;  // ㄴ
            case 3: return 47;  // ㄷ
            case 5: return 48;  // ㄹ
            case 6: return 56;  // ㅁ
            case 7: return 57;  // ㅂ
            case 9: return 59;  // ㅅ
            case 10: return 60; // ㅆ
            case 11: return 61; // ㅇ
            case 12: return 62; // ㅈ
            case 14: return 63; // ㅊ
            case 15: return 64; // ㅋ
            case 16: return 65; // ㅌ
            case 17: return 66; // ㅍ
            case 18: return 67; // ㅎ
        }
        return -1;
    }
    // 분해 
    private void DecomposeConsonant()       // 자음분해
    {
        int i = 0;
        if (m_nPhonemez[3] > 40 || m_nPhonemez[4] > 40)
        {
            do
            {
                if (DIVIDE_JONG_CONSON[i, 2] == m_nPhonemez[2])
                {
                    m_nPhonemez[3] = DIVIDE_JONG_CONSON[i, 0];
                    m_nPhonemez[4] = DIVIDE_JONG_CONSON[i, 1];

                    m_completeWord = CombineHangle(3);
                    m_nPhonemez[0] = ToInitial(m_nPhonemez[4]);
                    return;
                }
            }
            while (++i < 13);
        }

        m_completeWord = CombineHangle(1);
        m_nPhonemez[0] = ToInitial(m_nPhonemez[2]);
    }
    // 합성 
    private bool MixInitial(int nKeyCode)   // 초성합성
    {
        if (nKeyCode >= 19)
            return false;

        int i = 0;
        do
        {
            if (MIXED_CHO_CONSON[i, 0] == m_nPhonemez[0] && MIXED_CHO_CONSON[i, 1] == nKeyCode)
            {
                m_nPhonemez[0] = MIXED_CHO_CONSON[i, 2];
                return true;
            }
        }
        while (++i < 14);

        return false;
    }

    private bool MixFinal(int nKeyCode)     // 종성합성
    {
        if (nKeyCode <= 40) return false;

        int i = 0;
        do
        {
            if (MIXED_JONG_CONSON[i, 0] == m_nPhonemez[2] && MIXED_JONG_CONSON[i, 1] == nKeyCode)
            {
                m_nPhonemez[3] = m_nPhonemez[2];
                m_nPhonemez[4] = nKeyCode;
                m_nPhonemez[2] = MIXED_JONG_CONSON[i, 2];

                return true;
            }
        }
        while (++i < 22);

        return false;
    }

    unsafe private bool MixVowel(int* currentCode, int inputCode)   // 모음합성
    {
        int i = 0;
        do
        {
            if (MIXED_VOWEL[i, 0] == *currentCode && MIXED_VOWEL[i, 1] == inputCode)
            {
                *currentCode = MIXED_VOWEL[i, 2];
                return true;
            }
        }
        while (++i < 21);

        return false;
    }

    // 조합(한글완성) 
    private char CombineHangle(int cho, int jung, int jong)
    {
        // 초성 * 21 * 28 + (중성 - 19) * 28 + (종성 - 40) + BASE_CODE;
        return Convert.ToChar(BASE_CODE - 572 + jong + cho * 588 + jung * 28);
    }
    private char CombineHangle(int status)
    {
        switch (status)
        {
            //초성 + 중성
            case 1: return CombineHangle(m_nPhonemez[0], m_nPhonemez[1], 40);

            //초성 + 중성 + 종성
            case 2: return CombineHangle(m_nPhonemez[0], m_nPhonemez[1], m_nPhonemez[2]);

            //초성 + 중성 + 곁자음01
            case 3: return CombineHangle(m_nPhonemez[0], m_nPhonemez[1], m_nPhonemez[3]);
        }
        return ' ';
    }

    private void CombineIngWord(int status)
    {
        switch (m_nStatus)
        {
            case (int)HAN_STATUS.HS_FIRST:
            case (int)HAN_STATUS.HS_FIRST_V:
            case (int)HAN_STATUS.HS_FIRST_C:
                ingWord = SOUND_TABLE[m_nPhonemez[0]];
                break;

            case (int)HAN_STATUS.HS_MIDDLE_STATE:
            case (int)HAN_STATUS.HS_END:
                ingWord = CombineHangle(1);
                break;

            case (int)HAN_STATUS.HS_END_STATE:
            case (int)HAN_STATUS.HS_END_EXCEPTION:
                ingWord = CombineHangle(2);
                break;
        }
    }
    private int DownGradeIngWordStatus(char word)   //조합 문자 상태 낮추기
    {
        int iWord = word;

        //초성만 있는 경우
        if (iWord < LIMIT_MIN || iWord > LIMIT_MAX)
        {
            ingWord = '\0';

            return (int)HAN_STATUS.HS_FIRST;
        }

        //문자코드 체계
        //iWord = firstWord * (21*28)
        //		+ middleWord * 28
        //		+ lastWord * 27
        //		+ BASE_CODE;
        //
        int totalWord = iWord - BASE_CODE;
        int iFirstWord = totalWord / 28 / 21;   //초성
        int iMiddleWord = totalWord / 28 % 21;  //중성
        int iLastWord = totalWord % 28;     //종성

        m_nPhonemez[0] = iFirstWord; //초성저장

        if (iLastWord == 0) //종성이 없는 경우
        {
            ingWord = SOUND_TABLE[m_nPhonemez[0]];

            return (int)HAN_STATUS.HS_FIRST_V;
        }

        m_nPhonemez[1] = iMiddleWord + 19; //중성저장

        iLastWord += 40;

        for (int i = 0; i < 13; i++)
        {
            if (iLastWord == DIVIDE_JONG_CONSON[i, 2])
            {
                ingWord = CombineHangle(3);
                m_nPhonemez[2] = DIVIDE_JONG_CONSON[i, 0]; // 종성저장
                return (int)HAN_STATUS.HS_END_EXCEPTION;
            }
        }

        ingWord = CombineHangle(1);

        return (int)HAN_STATUS.HS_MIDDLE_STATE;
    }

    void AppendText(int nCode)
    {
        // 문자열 입력
        SetKeyCode(nCode);

        string strText = completeText;

        if (ingWord != '\0')
        {
            strText += ingWord;
        }
    }


    public void button_Click(int _idx)
    {
        AppendText(_idx);

        //AppendText(-3);// delete
        //AppendText(-1);// Space
        //AppendText(0);// ㄱㅋㄲ
        //AppendText(2);// ㄴ
        //AppendText(3);// ㄷㅌㄸ
        //AppendText(5);// ㄹ
        //AppendText(6);// ㅁ
        //AppendText(7);// ㅂㅍㅃ
        //AppendText(9);// ㅅㅆ
        //AppendText(11);// ㅇ
        //AppendText(12);// ㅈㅊㅉ
        //AppendText(18);// ㅎ
        //AppendText(19);// ㅏㅑ
        //AppendText(23);// ㅓㅕ
        //AppendText(27);// ㅗㅛ
        //AppendText(32);// ㅜㅠ
        //AppendText(39);// ㅡㅣ 
    }
    private void buttonClear_Click()
    {
        Clear();// clear
    }
}