using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Data;

namespace CRM.Application.Local
{
    public static class Workflow
    {
        public static List<WorkFlowNode> GetRequestProgressAndLevels(int requestTypeID)
        {
            List<WorkFlowNode> nodes = WorkFlowDB.GetAllWorkFlowNode(requestTypeID);

            WorkFlowNode startNode = nodes.Where(t => t.CurrentStatusTypeID == (byte)DB.RequestStatusType.Start).SingleOrDefault();

            TraceNodes(nodes, startNode.ID);

            List<WorkFlowNode> treeNodes = nodes.Where(t => t.IsIncludedInTree).ToList();

            foreach (WorkFlowNode item in treeNodes.Where(t => !t.ParentID.HasValue))
                UpdateLevel(treeNodes, item.ID, nodes.Max(t => t.Level));

            return treeNodes;
        }

        private static void TraceNodes(List<WorkFlowNode> nodes, long nodeID)
        {
            WorkFlowNode node = nodes.Where(t => t.ID == nodeID).Single();

            List<WorkFlowNode> nextNodes = nodes.Where(t => t.CurrentStatusID == node.NextStatusID).OrderByDescending(t => t.NextStatusTypeID).ToList();

            if (node.CurrentStatusTypeID == (byte)DB.RequestStatusType.Completed || node.NextStatusTypeID == (byte)DB.RequestStatusType.End || node.CurrentStatusTypeID == (byte)DB.RequestStatusType.Start)
                node.IsIncludedInTree = true;

            if (nextNodes.Count == 0 || node.NextStatusTypeID == (byte)DB.RequestStatusType.End)
                return;

            foreach (WorkFlowNode item in nextNodes)
                TraceNodes(nodes, item.ID);
        }

        private static void UpdateLevel(List<WorkFlowNode> nodes, long nodeID, int level)
        {
            WorkFlowNode node = nodes.Where(t => t.ID == nodeID).Single();

            List<WorkFlowNode> children = nodes.Where(t => t.ParentID == node.ID).OrderByDescending(t => t.NextStatusTypeID).ToList();

            node.Level = level + 1;

            foreach (WorkFlowNode child in children)
                UpdateLevel(nodes, child.ID, level + 1);
        }
    }
}
