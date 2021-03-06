
// https://eips.ethereum.org/EIPS/eip-20
// SPDX-License-Identifier: MIT
pragma solidity >=0.5.0 <0.8.0;
pragma abicoder v2;

contract ERC20Token{

    event Transfer(address indexed _from, address indexed _to, uint256 _value);
    event Approval(address indexed _owner, address indexed _spender, uint256 _value);
    event IssueToDonor(address indexed _donor, bytes indexed txnHashIndexed, uint _value, bytes txnHash);
    event IssuerChanged(address indexed _newIssuer, address indexed _oldIssuer);

    uint256 constant private MAX_UINT256 = 2**256 - 1;
    mapping (address => uint256) public balances;
    mapping (address => mapping (address => uint256)) public allowed;
    uint256 public totalSupply;
    /*
    NOTE:
    The following variables are OPTIONAL vanities. One does not have to include them.
    They allow one to customise the token contract & in no way influences the core functionality.
    Some wallets/interfaces might not even bother to look at this information.
    */
    string public name;                   
    uint8 public decimals;                
    string public symbol;
    address public issuer;

    struct DonorIssuance {
        address donor;
        uint value;
        bytes txnHashDonation;
    }                 

    constructor(string memory _tokenName, uint8 _decimalUnits, string  memory _tokenSymbol) {
        totalSupply = 0;
        issuer = msg.sender;
        name = _tokenName;                                   // Set the name for display purposes
        decimals = _decimalUnits;                            // Amount of decimals for display purposes
        symbol = _tokenSymbol;                               // Set the symbol for display purposes
    }

    modifier onlyIssuer {
        if (msg.sender != issuer) {
        revert("Only Issuer");
        }
        _;
    }

    function issueTokenToDonor(DonorIssuance[] memory _donorIssuance) onlyIssuer public returns (bool success)  {

        for (uint index = 0; index < _donorIssuance.length; index++) {
            DonorIssuance memory current = _donorIssuance[index];
            totalSupply += current.value;
            balances[current.donor] += current.value;
            emit IssueToDonor(current.donor, current.txnHashDonation, current.value, current.txnHashDonation);
        }    
        return true;
    }

    function changeIssuer(address payable _newIssuer) public onlyIssuer {
        if(_newIssuer != issuer) {
            address oldIssuer = issuer;
            issuer = _newIssuer;
            emit IssuerChanged(_newIssuer, oldIssuer);
        }
    }

    function transfer(address _to, uint256 _value) public  returns (bool success) {
        require(balances[msg.sender] >= _value, "token balance is lower than the value requested");
        balances[msg.sender] -= _value;
        balances[_to] += _value;
        emit Transfer(msg.sender, _to, _value); //solhint-disable-line indent, no-unused-vars
        return true;
    }

    function transferFrom(address _from, address _to, uint256 _value) public returns (bool success) {
        uint256 allowance = allowed[_from][msg.sender];
        require(balances[_from] >= _value && allowance >= _value, "token balance or allowance is lower than amount requested");
        balances[_to] += _value;
        balances[_from] -= _value;
        if (allowance < MAX_UINT256) {
            allowed[_from][msg.sender] -= _value;
        }
        emit Transfer(_from, _to, _value); //solhint-disable-line indent, no-unused-vars
        return true;
    }

    function balanceOf(address _owner) public view returns (uint256 balance) {
        return balances[_owner];
    }

    function approve(address _spender, uint256 _value) public returns (bool success) {
        allowed[msg.sender][_spender] = _value;
        emit Approval(msg.sender, _spender, _value); //solhint-disable-line indent, no-unused-vars
        return true;
    }

    function allowance(address _owner, address _spender) public view returns (uint256 remaining) {
        return allowed[_owner][_spender];
    }
}