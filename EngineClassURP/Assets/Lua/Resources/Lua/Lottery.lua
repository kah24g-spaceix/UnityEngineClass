math.randomseed(os.time())

function DoLottery()
    local number = math.random(1000)
    if number < 300 then
        return true
    else
        return false
    end
end