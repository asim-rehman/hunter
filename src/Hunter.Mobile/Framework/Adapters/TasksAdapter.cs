using Android.Content;
using Android.Views;
using Android.Widget;
using System;

namespace Hunter.Mobile.Framework.Adapters
{
    public class TasksAdapter : ArrayAdapter<Models.Tasks>
    {
        Context context;
        LayoutInflater layoutInflater;
        Models.Tasks[] tasks;
        public TasksAdapter(Context context, Models.Tasks[] tasks) : base(context, 0, tasks)
        {
            this.context = context;
            this.tasks = tasks;
            layoutInflater = LayoutInflater.From(context);
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if(convertView==null)
            {
                convertView = layoutInflater.Inflate(Resource.Layout.tasks_adapter, parent, false);
            }

            Models.Tasks task = GetItem(position);
            TextView tvId = convertView.FindViewById<TextView>(Resource.Id.tvTaskId);
            TextView tvTaskType = convertView.FindViewById<TextView>(Resource.Id.tvTaskType);
            TextView tvNextRun = convertView.FindViewById<TextView>(Resource.Id.tvNextRun);
            TextView tvLastRun = convertView.FindViewById<TextView>(Resource.Id.tvLastRun);

            tvId.SetText("ID: " + task.Id.ToString(),TextView.BufferType.Normal);
            tvTaskType.SetText("Task Type: " + Enum.GetName(typeof(Enums.TaskType), task.TaskType), TextView.BufferType.Normal);
            tvNextRun.SetText("Next Run: "+ task.NextRun.ToString(), TextView.BufferType.Normal);
            tvLastRun.SetText("Last Run: " + task.LastRun.ToString(), TextView.BufferType.Normal);

            return convertView;
        }
        public Models.Tasks GetItem(int position)
        {
            return tasks[position];
        }
    }
}