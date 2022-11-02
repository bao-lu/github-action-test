/* © Copyright HCL Technologies Ltd. 2022 */

const core = require('@actions/core');

const findingsByFileMap = [{
      "file": "AntiForgeryTokenAspnet.aspx",
      "filePath": "AntiForgeryTokenAspnet.aspx",
      "lineNumber": 32,
      "columnNumber": 0,
      "language": "ASP.NET",
      "vulnName": "Ignoring antiforgery token leads to CSRF",
      "vulnType": "CrossSiteRequestForgery",
      "ruleName": "com.hcl.appscan.scanner.aspnet.rules.AntiForgeryTokenAspnet",
      "context": "[IgnoreAntiforgeryToken]",
      "severity": 0,
      "hashValues": {
        "0": -1123556723,
        "1": 168593624,
        "2": -2116264308,
        "3": -2116264308
      	}
	  },
  	  {"file": "AntiForgeryTokenAspnet.aspx",
      "filePath": "AntiForgeryTokenAspnet.aspx",
      "lineNumber": 32,
      "columnNumber": 0,
      "language": "ASP.NET",
      "vulnName": "Ignoring antiforgery token leads to CSRF",
      "vulnType": "CrossSiteRequestForgery",
      "ruleName": "com.hcl.appscan.scanner.aspnet.rules.AntiForgeryTokenAspnet",
      "context": "[IgnoreAntiforgeryToken]",
      "severity": 1,
      "hashValues": {
        "0": -1123556723,
        "1": 168593624,
        "2": -2116264308,
        "3": -2116264308
       }
      },
  	  {"file": "AntiForgeryTokenAspnet.aspx",
      "filePath": "AntiForgeryTokenAspnet.aspx",
      "lineNumber": 32,
      "columnNumber": 0,
      "language": "ASP.NET",
      "vulnName": "Ignoring antiforgery token leads to CSRF",
      "vulnType": "CrossSiteRequestForgery",
      "ruleName": "com.hcl.appscan.scanner.aspnet.rules.AntiForgeryTokenAspnet",
      "context": "[IgnoreAntiforgeryToken]",
      "severity": 0,
      "hashValues": {
        "0": -1123556723,
        "1": 168593624,
        "2": -2116264308,
        "3": -2116264308
       }
      }];

if(findingsByFileMap.size === 0) {
            resolve(0);
            return;
        }
		
		let high_issues = 0;
		let medium_issues = 0;
		let low_issues = 0;
		let info_issues = 0;
		let total_issues = 0;
		
		core.info('Creating summary page...');
		
		for (const [severity, findingsArray] of findingsByFileMap.entries()){
			switch(findingsArray.severity){
				case 0:
					high_issues++;
					break;
				case 1:
					medium_issues++;
					break;
				case 2:
					low_issues++;
					break;
				case 3:
					info_issues++;
					break;
				default:
					break;
			}
		}
		
		total_issues = high_issues + medium_issues + low_issues + info_issues;
        
        await core.summary
		.addHeading('Scan results')
		.addTable([
		  [{data: 'Scan', header: true}, {data: 'Result', header: true}],
		  ['High', 'Medium', 'Low', 'Info', 'Total Findings'],
		  [ high_issues, medium_issues, low_issues, info_issues, total_issues]
		])
		.write()