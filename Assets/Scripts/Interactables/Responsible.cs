using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Responsible : Interactable
{
	private bool _jobFinished;
	
	protected NavMeshAgent agent;
	protected List<IEnumerator> jobList;
	protected List<Interactable> targetList;
	protected List<GameObject> buttonList;

	public bool targetInRange;
	public GameObject target;

	public List<IEnumerator> JobList
	{
		get { return jobList; }
		set { jobList = value; }
	}

	public List<Interactable> TargetList
	{
		get { return targetList; }
		set { targetList = value; }
	}

	public bool JobFinished
	{
		get { return _jobFinished; }
		set { _jobFinished = value; }
	}

	private void Start()
	{
		this.Name = "Moruk";
		jobList =  new List<IEnumerator>();
		targetList = new List<Interactable>();
		buttonList = new List<GameObject>();
		agent = GetComponent<NavMeshAgent>();
		_jobFinished = true;
	}

	private void Update()
	{
		DoJob();
	}

	public IEnumerator Walk(Vector3 position)
	{
		agent.isStopped = false;
		agent.SetDestination(position);
		yield return new WaitUntil((() => targetInRange));
		StopWalking();
	}

	private void StopWalking()
	{
		agent.isStopped = true;
		agent.ResetPath();
	}

	public void AddJob(IEnumerator job)
	{
		jobList.Add(job);
	}

	public void RemoveJob(IEnumerator job)
	{
		jobList.RemoveAt(jobList.IndexOf(job));
	}

	public void AddTarget(Interactable target)
	{
		targetList.Add(target);
	}

	public void RemoveTarget(int index)
	{
		targetList.RemoveAt(index);
	}

	public void AddButton(GameObject button)
	{
		buttonList.Add(button);
	}

	public void RemoveButton(int index)
	{
		ButtonUtil.Destroy(buttonList[index].gameObject);
		buttonList.RemoveAt(index);
	}

	private void DoJob()
	{
		if (_jobFinished && jobList.Count != 0)
		{
			_jobFinished = false;
			target = targetList[0].gameObject;
			targetInRange = false;
			
			StartCoroutine(jobList[0]);
			
			jobList.RemoveAt(0);
			targetList.RemoveAt(0);
		}
	}

	public void StopDoingJob(IEnumerator job)
	{
		if (!jobList.Contains(job))
		{
			StopCoroutine(job);
			StopWalking();
			FinishJob();
		}
		else
		{
			RemoveTarget(jobList.IndexOf(job));
			RemoveJob(job);
		}
	}

	public void FinishJob()
	{
		_jobFinished = true;
		RemoveButton(0);
	}
}
