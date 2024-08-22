using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public enum Mode
{
    A, B, C
}
//인스펙터에서 여러 개를 선택이 가능합니다. [Flags]
[Flags]
public enum Mode2
{
    None = 0, //아무것도 선택되지 않았음을 표현
    A = 1 << 0, //1
    B = 1 << 1, //2
    C = 1 << 2, //4
    D = 1 << 3  //8

    //비트 연산자
    //비트 연산에 대한 작업을 진행할 때 사용하는 연산자입니다.

    //shift 연산자 <<, >>는 비트를 이동시키는 역할을 가지고 있습니다.

    //예시) 5 >> 2
    //10진수 -> 2진수로 계산
    //     수   8421
    //     5     101
    // 5 >> 2    001 | 01
    //             1

}
//유니티에서 사용할 수 있는 일반적인 기능들에 대한 클래스

//해당 기능을 추가하면 유니티 에디터 쪽의 AddComponentMenu의 항목으로 설정됩니다.
[AddComponentMenu("DataBasic/DataBasic1")]
public class DataBasic : MonoBehaviour
{
    //유니티 기본 규칙
    //1. public으로 작성한 필드 값은 유니티의 인스펙ㅌ에
    //   공개가 됩니다.
    //2. public으로 공개된 값들은 유니티 인스펙터에 작성한 값을
    //   최우선시합니다.
    //3. 배열이나 리스트 생성 같이 new를 쓰는 경우
    //   적용이 됩니다.

    public int value;
    [TextArea(1, 5)] string word;
    public char alpha;
    public float value2;
    public bool isdead;

    [ContextMenuItem("배열 초기화", "ArrayReset")]
    public int[] array;
    //해당 필드에서 오른쪽 버튼을 누르면 설명 창이 뜨고, 누르면 넣어준 함수를 실행합니다.

    public List<int> int_list;
    public Mode mode;
    public Mode2 mode2;

    private void Start()
    {
        
    }

    //컴포넌트에서 오른쪽 버튼을 눌렀을 경우 나오는 기능
    [ContextMenu("데이터 설정")]
    void DataSetting()
    {
        value = 5;
        word = "헬로우";
        alpha = 'a';
        value2 = 1.34f;
        isdead = true;
        array = new int[5];
        int_list = new List<int>();
        mode = Mode.A;
        mode2 = Mode2.A | Mode2.B;
    }

    void ArrayReset()
    {
        array = new int[5];
    }
}
