namespace Bat.Queue;

public enum RabbitExchangeType : byte
{
    Direct = 1,

    Fanout = 2,

    Headers = 3,

    Topic = 4
}