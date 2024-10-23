using UnityEngine;

[ExecuteInEditMode]
public class ImageEffectBlur : MonoBehaviour
{
	[SerializeField] private Material material;

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		RenderTexture tmp = RenderTexture.GetTemporary(src.width, src.height, 0, src.format);

		Graphics.Blit(src, tmp, material, 0);	// First pass.
		Graphics.Blit(tmp, dest, material, 1);	// Second pass.

		RenderTexture.ReleaseTemporary(tmp);
	}
}
