using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour {
	private const int POSITION_SPEED = 500;
	private const int SCALE_SPEED = 2;
	private Text messageText;
	private string finalText;
	private RectTransform holderTransform;
	private RectTransform iconTransform;
	private RectTransform actionIconTransform;
	private Vector3 iconTargetPosition;
	private string[] messageLines;
	private int currentLineIndex;
	private float messageIconTimer = -Mathf.PI/2;
	private float actionIconTimer = -Mathf.PI/2;
	private bool isClosing;

	public RawImage messageHolder;
	public RawImage messageIcon;
	public RawImage messageActionIcon;

	// Use this for initialization
	public void init (string[] lines) {
		messageLines = lines;
		messageText = messageHolder.GetComponentInChildren<Text> ();
		messageText.text = "";

		finalText = messageLines[0];
		currentLineIndex = 0;

		if (messageLines.Length > 1) {
			messageActionIcon.texture = Resources.Load ("message_next_icon") as Texture;
		}

		//Set transforms to hide hide (reset) message
		holderTransform = messageHolder.GetComponent<RectTransform> ();
		holderTransform.localScale = new Vector3 (0, 1, 1);

		iconTransform = messageIcon.GetComponent<RectTransform> ();
		iconTransform.localScale = new Vector3 (0, 0, 1);

		iconTargetPosition = new Vector3(iconTransform.localPosition.x, iconTransform.localPosition.y, iconTransform.localPosition.z); 
		iconTransform.localPosition = new Vector3 (0, iconTargetPosition.y, iconTargetPosition.z);

		actionIconTransform = messageActionIcon.GetComponent<RectTransform> ();
		actionIconTransform.localScale = new Vector3 ();

		isClosing = false;

		messageHolder.enabled = true;
		messageActionIcon.enabled = true;

		Color textColor = messageText.color;
		textColor.a = 1;
		messageText.color = textColor;
	}
		
	// Update is called once per frame
	void Update () {
		if (isClosing) {
			//Action button shrinks
			if (actionIconTimer > 0) {
				actionIconTimer -= SCALE_SPEED*5*Time.deltaTime;
				float size = 1 + Mathf.Sin (actionIconTimer);
				actionIconTransform.localScale = new Vector3 (size, size, iconTransform.localScale.z);
				return;
			}

			messageActionIcon.enabled = false;

			//Fade text, shrink holder, move icon
			if (messageText.color.a > 0) {
				Color textColor = messageText.color;
				textColor.a -= 2*Time.deltaTime;
				messageText.color = textColor;
			}

			if (holderTransform.localScale.x > 0) {
				holderTransform.localScale -= SCALE_SPEED * (new Vector3 (Time.deltaTime, 0, 0));
			}

			if (iconTransform.localPosition.x < 0) {
				iconTransform.localPosition += POSITION_SPEED * (new Vector3 (Time.deltaTime, 0, 0));
				return;
			}

			messageHolder.enabled = false;

			//Shrink icon
			if (messageIconTimer > 0) {
				messageIconTimer -= SCALE_SPEED*5*Time.deltaTime;
				float size = 1 + Mathf.Sin (messageIconTimer);
				iconTransform.localScale = new Vector3 (size, size, iconTransform.localScale.z);
				return;
			}

			iconTransform.localPosition = iconTargetPosition;
			MasterMonoBehaviour.Instance.TerminateMessage ();
		} 
		else {
			//Icon grows
			if (messageIconTimer < Mathf.PI) {
				messageIconTimer += SCALE_SPEED*5*Time.deltaTime;
				float size = 1 + Mathf.Sin (messageIconTimer);
				iconTransform.localScale = new Vector3 (size, size, iconTransform.localScale.z);
				return;
			}

			//Icon moves
			if (iconTransform.localPosition.x > iconTargetPosition.x) {
				iconTransform.localPosition -= POSITION_SPEED * (new Vector3 (Time.deltaTime, 0, 0));
			}

			//Holder grows
			if (holderTransform.localScale.x < 1) {
				holderTransform.localScale += SCALE_SPEED * (new Vector3 (Time.deltaTime, 0, 0));
			}

			//Text animates
			if (finalText.Length > 0) {
				messageText.text += finalText [0];
				finalText = finalText.Remove (0, 1);
				return;
			}
				
			//Action icon grows
			if (actionIconTimer < Mathf.PI) {
				actionIconTimer += SCALE_SPEED*5*Time.deltaTime;
				float size = 1 + Mathf.Sin (actionIconTimer);
				actionIconTransform.localScale = new Vector3 (size, size, iconTransform.localScale.z);
				return;
			}

			//Check input
			if (Input.GetKeyDown (KeyCode.Return)) {
				if (currentLineIndex == messageLines.Length - 1) {
					isClosing = true;
				} else {
					currentLineIndex++;
					messageText.text = "";
					finalText = messageLines [currentLineIndex];

					if (currentLineIndex == messageLines.Length - 1) {
						messageActionIcon.texture = Resources.Load ("message_exit_icon") as Texture;
					}
				}
			}
		}
	}
}
