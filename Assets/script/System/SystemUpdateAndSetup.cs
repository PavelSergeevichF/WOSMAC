using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemUpdateAndSetup : MonoBehaviour
{
	private List<ITick> ticks = new List<ITick>();
	private List<ITickFixed> ticksFixes = new List<ITickFixed>();
	private List<ITickLate> ticksLate = new List<ITickLate>();


	private void Start()
	{ }
    private void Update()
    {
		for (var i = 0; i < ticks.Count; i++)
		{
			ticks[i].Tick();
		}
	}

    private void FixedUpdate()
    {
		for (var i = 0; i < ticksFixes.Count; i++)
		{
			ticksFixes[i].TickFixed();
		}
	}

    private void LateUpdate()
    {
		for (var i = 0; i < ticksLate.Count; i++)
		{
			ticksLate[i].TickLate();
		}
	}

	

}
