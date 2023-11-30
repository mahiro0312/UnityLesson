using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_NonPhysics2D : MonoBehaviour
{

    //�t�B�[���h 
    //Inspector�ł̒���
    public float speed = 15.0f; //Player�̑��x
    public Sprite[] run; //����X�v���C�g
    public Sprite[] jump;�@//�W�����v�X�v���C�g

    //���[�J���ϐ�   
    float jumpVy;//�v���C���[�̏㏸���x
    int animIndex; //�A�j���Đ��C���f�b�N�X
    bool goalCheck; //�S�[������

    // Start is called before the first frame update
    void Start()
    {
        //������
        jumpVy = 0.0f;
        animIndex = 0;
        goalCheck = false;
    }

    //Player�ɑ��̃I�u�W�F�N�g�̃R���W������������
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        //�S�[���`�F�b�N
        if (collision.gameObject.name == "Stage_Gate")
        {
            goalCheck = true;
            return;
        }
        //�S�[���łȂ���΃X�e�[�W���ēǂݍ��݂��ă��Z�b�g
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (goalCheck)//�S�[���Ȃ��~
        {
            return;
        }

        //���݂̃v���C���[�L�����̍������v�Z
        float height = transform.position.y + jumpVy;

        //�ڒn�`�F�b�N
        if(height <= 0.0f)
        {
            //�W�����v������
            height = 0.0f;
            jumpVy = 0.0f;

            //�W�����v�`�F�b�N
            if (Input.GetButtonDown("Fire1"))
            {
                //�W�����v����
                jumpVy = +1.3f;

                //�W�����v�X�v���C�g�̉摜�ɐ؂�ւ�
                GetComponent<SpriteRenderer>().sprite = jump[0];
            }
            else
            {
                //���菈��
                animIndex++;
                if(animIndex >= run.Length)
                {
                    animIndex = 0;
                }

                //����X�v���C�g�摜�ɐ؂�ւ�
                GetComponent<SpriteRenderer>().sprite = run[animIndex];
            }
        }
        else
        {
            //�W�����v��̍~����
            //jumpVy -= 0.2f;
            jumpVy -= 6.0f * Time.deltaTime;
        }


        //�v���C���[�̃L�����ړ�(���W�ݒ�)
        transform.position = new Vector3(
            transform.position.x + speed * Time.deltaTime, height, 0.0f);

        //�J�����̈ړ�(���W�𑊑Έړ�)
        GameObject goCam = GameObject.Find("Main Camera");
        goCam.transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
    }

    //GUI�̕\��
    void OnGUI()
    {
        //�f�o�b�N�e�L�X�g
        GUI.TextField(new Rect(10, 10, 300, 60),
            "[Unity 2D Sample 3-1 A]\n�}�E�X�̍��{�^�����}�E�X�̍��{�^���������Ɖ���\n�͂Ȃ��ƃW�����v");

        //���Z�b�g�{�^��
        if(GUI.Button(new Rect(10, 80, 100, 20), "���Z�b�g"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
