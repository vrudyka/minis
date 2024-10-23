using UnityEngine;

public class BlurPulse : MonoBehaviour
{
	[SerializeField] private Material material;
	[SerializeField] private float speed;

	private readonly int idSigma = Shader.PropertyToID("_Sigma");
	private readonly int idKernel = Shader.PropertyToID("_KernelSize");

	private void Update()
	{
		var s = Mathf.Sin(Time.time * speed) * 10f + 10f;
		s = Mathf.Clamp(s, 1f, 20f);
		material.SetFloat(idSigma, s);

		var k = Mathf.Sin(Time.time * speed) * 50f + 50f;
		k = Mathf.Clamp(k, 1f, 100f);
		material.SetFloat(idKernel, k);
	}
}
