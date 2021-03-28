const calculate = (value) => {
    let result = 1

    for (let numberToTest = 2; numberToTest <= value; numberToTest++) {
        let isPrime = true
        for (let del = 2; del < numberToTest; del++) {
            if (numberToTest % del === 0) {
                isPrime = false
                break
            }
        }
        if (isPrime) {
            result = numberToTest
        }
    }

    return result
}


self.addEventListener('message', function(event){
    const max = event.data.max
    const result = calculate(max)
    console.log('Service worker', result)
    
    // somehow pass to page
});