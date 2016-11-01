using System;
using System.Collections.Generic;

using ColossalFramework.IO;

namespace PropPrecision
{
    public class Data : IDataContainer
    {
        public class PrecisionData
        {
            public ushort x;
            public ushort z;
        }

        public static Dictionary<ushort, PrecisionData> data = new Dictionary<ushort, PrecisionData>();

        public void Serialize(DataSerializer s)
        {
            try
            {
                int count = 0;
                foreach (ushort prop in data.Keys)
                {
                    if ((PropManager.instance.m_props.m_buffer[prop].m_flags & (ushort)PropInstance.Flags.Created) == (ushort)PropInstance.Flags.Created)
                    {
                        count++;
                    }
                }

                s.WriteInt32(count);
                foreach(ushort prop in data.Keys)
                {
                    if ((PropManager.instance.m_props.m_buffer[prop].m_flags & (ushort)PropInstance.Flags.Created) == (ushort)PropInstance.Flags.Created)
                    {
                        s.WriteUInt16(prop);
                        s.WriteUInt16(data[prop].x);
                        s.WriteUInt16(data[prop].z);
                    }
                }
            }
            catch(Exception e)
            {
                DebugUtils.LogException(e);
            }
        }

        public void Deserialize(DataSerializer s)
        {
            try
            {
                data.Clear();

                var arraySize = s.ReadInt32();

                for (int i = 0; i < arraySize; i++)
                {
                    ushort prop = (ushort)s.ReadUInt16();

                    PrecisionData value = new PrecisionData();
                    value.x = (ushort)s.ReadUInt16();
                    value.z = (ushort)s.ReadUInt16();

                    if ((PropManager.instance.m_props.m_buffer[prop].m_flags & (ushort)PropInstance.Flags.Created) == (ushort)PropInstance.Flags.Created)
                    {
                        data[prop] = value;
                    }
                }
            }
            catch (Exception e)
            {
                DebugUtils.LogException(e);
            }
        }

        public void AfterDeserialize(DataSerializer s)
        {
        }
    }
}