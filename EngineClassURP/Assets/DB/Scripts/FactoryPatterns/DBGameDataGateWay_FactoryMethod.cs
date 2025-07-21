using System;
using System.Collections.Generic;
using System.Data;
public abstract class DBGameDataGateWay_AbstractFactory : IGameDataGateWay
{
    private DBAccess m_access = new DBAccess();
    private readonly DBConnectionFactoryMethod m_factory = new DBConnectionFactoryMethod();
    protected abstract IDbConnection CreateConnection();
    public CharacterState[] LoadCharacters()
    {
        List<CharacterState> states = new List<CharacterState>();
        m_access.Read(
            m_factory.CreateConnection(),
            reader =>
            {
                Int32 id = reader.GetInt32(0);
                String name = reader.GetString(1);
                Int32 hp = reader.GetInt32(2);
                Int32 attack = reader.GetInt32(3);

                states.Add(new CharacterState(id, name, attack, hp));
            },
            "select * from character_table"
            );
        return states.ToArray();
    }

    public ItemData[] LoadItems()
    {
        throw new System.NotImplementedException();
    }
}