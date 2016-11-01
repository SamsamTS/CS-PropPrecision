using System.Reflection;
using ColossalFramework;
using PropPrecision.Redirection;
using UnityEngine;

namespace PropPrecision
{
    [TargetType(typeof(PropInstance))]
    public struct PropInstanceDetour
    {
        [RedirectMethod]
        public unsafe Vector3 Position
        {
            get
            {
                fixed (void* pointer = &this)
                {
                    PropInstance* prop = (PropInstance*)pointer;

                    if (Singleton<ToolManager>.instance.m_properties.m_mode == ItemClass.Availability.AssetEditor)
                    {
                        Vector3 result;
                        result.x = (float)prop->m_posX * 0.0164794922f;
                        result.y = (float)prop->m_posY * 0.015625f;
                        result.z = (float)prop->m_posZ * 0.0164794922f;
                        return result;
                    }
                    else
                    {
                        Vector3 result;
                        //Begin Mod
                        //result.x = (float)this.m_posX * 0.263671875f;
                        //result.y = (float)this.m_posY * 0.015625f;
                        //result.z = (float)this.m_posZ * 0.263671875f;

                        ushort index;
                        fixed (PropInstance* buffer = PropManager.instance.m_props.m_buffer)
                        {
                            index = (ushort)(prop - buffer);
                        }

                        if (Data.data.ContainsKey(index))
                        {
                            Data.PrecisionData precisionData = (Data.PrecisionData)Data.data[index];

                            if (prop->m_posX > 0)
                            {
                                result.x = ((float)prop->m_posX + (float)precisionData.x / (float)ushort.MaxValue) * 0.263671875f;
                            }
                            else
                            {
                                result.x = ((float)prop->m_posX - (float)precisionData.x / (float)ushort.MaxValue) * 0.263671875f;
                            }

                            if (prop->m_posZ > 0)
                            {
                                result.z = ((float)prop->m_posZ + (float)precisionData.z / (float)ushort.MaxValue) * 0.263671875f;
                            }
                            else
                            {
                                result.z = ((float)prop->m_posZ - (float)precisionData.z / (float)ushort.MaxValue) * 0.263671875f;
                            }
                            result.y = (float)prop->m_posY * 0.015625f;
                        }
                        else
                        {
                            result.x = (float)prop->m_posX * 0.263671875f;
                            result.y = (float)prop->m_posY * 0.015625f;
                            result.z = (float)prop->m_posZ * 0.263671875f;
                        }
                        //EndMod
                        return result;
                    }
                }
            }
            set
            {
                fixed (void* pointer = &this)
                {
                    PropInstance* prop = (PropInstance*)pointer;

                    if (Singleton<ToolManager>.instance.m_properties.m_mode == ItemClass.Availability.AssetEditor)
                    {
                        prop->m_posX = (short)Mathf.Clamp(Mathf.RoundToInt(value.x * 60.68148f), -32767, 32767);
                        prop->m_posZ = (short)Mathf.Clamp(Mathf.RoundToInt(value.z * 60.68148f), -32767, 32767);
                        prop->m_posY = (ushort)Mathf.Clamp(Mathf.RoundToInt(value.y * 64f), 0, 65535);
                    }
                    else
                    {
                        prop->m_posX = (short)Mathf.Clamp(/* Mathf.RoundToInt */(int)(value.x * 3.79259253f), -32767, 32767);
                        prop->m_posZ = (short)Mathf.Clamp(/* Mathf.RoundToInt */(int)(value.z * 3.79259253f), -32767, 32767);
                        prop->m_posY = (ushort)Mathf.Clamp(Mathf.RoundToInt(value.y * 64f), 0, 65535);
                        //Begin Mod
                        Data.PrecisionData precisionData = new Data.PrecisionData();
                        precisionData.x = (ushort)(ushort.MaxValue * Mathf.Abs(value.x * 3.79259253f - (float)prop->m_posX));
                        precisionData.z = (ushort)(ushort.MaxValue * Mathf.Abs(value.z * 3.79259253f - (float)prop->m_posZ));

                        fixed (PropInstance* buffer = PropManager.instance.m_props.m_buffer)
                        {
                            Data.data[(ushort)(prop - buffer)] = precisionData;
                        }
                        //EndMod
                    }
                }
            }
        }
    }
}