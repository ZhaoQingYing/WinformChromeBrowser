(function () {

    if (window.amazonBoundEvent) {
        var unameElem = document.getElementById('ap_email');
        var passwordElem = document.getElementById('ap_password');

        
        unameElem.value = window.amazonBoundEvent.aBName;
        passwordElem.value = window.amazonBoundEvent.aBPassword;
    }
    

    var elem = document.getElementById('signInSubmit');

    if (elem) {
        elem.addEventListener('click', function (e) {
            if (!window.amazonBoundEvent) {
                console.log('window.amazonBoundEvent does not exist.');
                return;
            }

            var uname = document.getElementById('ap_email').value;
            var password = document.getElementById('ap_password').value;

            if (uname == "" && password == "")
                return;

            if (uname == "" || password == "")
                return;

            window.amazonBoundEvent.raiseEvent('click', { unameField: uname, upasswordField: password });
        });
        //console.log(`Added click listener to ${elem.id}.`);
    }
})();