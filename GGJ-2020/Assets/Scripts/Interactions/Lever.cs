using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    // private Animator animator;

    bool isOnPosition = false;



    public int speed;
    public Vector3 startRotation = new Vector3(0, 0, 0);
    public Vector3 endRotation = new Vector3(0, 0, 0);

    public ConsoleStatus.PanelId panelId;
    public ConsoleStatus.ControlType controlType;



    // Start is called before the first frame update
    void Start()
    {
        this.transform.localRotation = Quaternion.Euler(startRotation.x, startRotation.y, startRotation.z);
        ConsoleStatus.consoleStatus.GetStatus(
            panelId,
            controlType
        );
    }

    void OnMouseDown()
    {
        bool isActive = ConsoleStatus.consoleStatus.GetStatus(
            panelId,
            controlType
        );
        Quaternion target = isActive ?
            Quaternion.Euler(endRotation.x, endRotation.y, endRotation.z) :
            Quaternion.Euler(startRotation.x, startRotation.y, startRotation.z);
        StartCoroutine(Move(target));
    }

    // Update is called once per frame
    void Update()
    {
        // NO-OP
    }

    public IEnumerator Move(Quaternion target)
    {

        for (float i = 0; i < 1; i += speed * Time.deltaTime)
        {
            transform.localRotation = Quaternion.Lerp(this.transform.localRotation, target, i);

            if (i > 0.8)
            {
                ConsoleStatus.consoleStatus.ToggleStatus(panelId, controlType);
            }

            yield return null;
        }
    }
}
