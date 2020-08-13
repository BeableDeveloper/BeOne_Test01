using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Native : MonoBehaviour
{
    public Image profileImage;

    // Start is called before the first frame update
    public void Image()
    {

        NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, 265);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                profileImage.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);

            }
        }, "Select png image ", "image/*", 265);
    }


    public void OnChangePhotoButtonClick()
    {
        // Don't attempt to pick media from Gallery/Photos if
        // another media pick operation is already in progress
        if (NativeGallery.IsMediaPickerBusy())
            return;

        // Pick a PNG image from Gallery/Photos
        // If the selected image's width and/or height is greater than 512px, down-scale the image

    }

    // Update is called once per frame
    void Update()
    {

    }
}
